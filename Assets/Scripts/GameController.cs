using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scoreMenu;
    [SerializeField] private CanvasGroup scorePanel;
    [SerializeField] private AudioClip die;
    public float varY;
    public List<float> speedPlatforms;
    private GameObject plat;
    private float lastY = 6.14f;
    private float score = 0f;
    private float leftLimit = -7.11f;
    private float rightLimit = 7.11f;
    void Start()
    {
        //speedPlatforms = new List<float> { 0f, 2f, 1f };
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > score)
        {
            score = transform.position.y;
        }
        scoreText.text = Mathf.Round(score).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Platform"))
        {
            int random = Random.Range(1, 10);
            int index;
            if (random == 1)
            {
                //y + 7.5, y + 11
                index = 2;
            }
            else if (random > 1 && random < 4)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }
            plat = (GameObject)Instantiate(platforms[index], new Vector2(Random.Range(leftLimit, rightLimit), lastY + varY), Quaternion.identity);
            lastY = plat.transform.position.y;
            Platform platformController = plat.GetComponent<Platform>();
            if (platformController != null)
            {
                platformController.horizontalSpeed = speedPlatforms[index];
            }
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            AudioController.instance.Reproduce(die);
            plat = collision.gameObject;
            Animator anim = plat.GetComponent<Animator>();
            Rigidbody2D rb = plat.GetComponent<Rigidbody2D>();
            PlayerMovement playerInputComponent = plat.GetComponent<PlayerMovement>();
            playerInputComponent.enabled = false;
            rb.velocity = new Vector2(0,0.5f);
            rb.gravityScale = 0.1f;
            anim.SetTrigger("Die");

            Invoke("FinishLevel", 1f);
        }
    }

    private void FinishLevel()
    {
        scoreMenu.SetActive(true);
        LeanTween.alphaCanvas(scorePanel, 1f, 0.8f)
                 .setEase(LeanTweenType.easeInQuad);
        plat.SetActive(false);
    }
}
