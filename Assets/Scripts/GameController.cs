using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private Score score;
    [SerializeField] private GameObject scoreMenu;
    [SerializeField] private CanvasGroup scorePanel;
    [SerializeField] private AudioClip die;
    public float varY;
    public List<float> speedPlatforms;
    public int recurrenceCoin;
    private GameObject plat;
    private GameObject coin;
    private float lastY = 6.14f;
    private float scoreAux = 0f;
    private float leftLimit = -7.11f;
    private float rightLimit = 7.11f;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreAux += 5f;

            score.additionalScore += 5f;

            UpdateScoreText();
        }

        if (transform.position.y > scoreAux - score.additionalScore)
        {
            scoreAux = transform.position.y;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        float totalScore = scoreAux + score.additionalScore;

        score.score = Mathf.RoundToInt(totalScore);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Platform"))
        {
            int random = Random.Range(1, 11);
            int randomCoin = Random.Range(1, recurrenceCoin);
            int index;
            if (random == 1)
            {
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
            
            if(randomCoin == 1)
            {
                coin = (GameObject)Instantiate(coins[index], new Vector2(Random.Range(leftLimit, rightLimit), lastY + (varY/2)), Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            AudioController.instance.Reproduce(die);
            GameObject player = collision.gameObject;
            Animator anim = player.GetComponent<Animator>();
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            PlayerMovement playerInputComponent = player.GetComponent<PlayerMovement>();
            playerInputComponent.enabled = false;
            rb.velocity = new Vector2(0f,0f);
            rb.gravityScale = 0.1f;
            anim.SetTrigger("Die");

            Invoke("FinishLevel", 1f);
        }
        else if (collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
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
