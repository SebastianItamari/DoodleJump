using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public List<GameObject> platforms;
    public TextMeshProUGUI scoreText;
    public GameObject scoreMenu;
    public float varY;// = 2.5f;
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
            collision.gameObject.SetActive(false);
            Time.timeScale = 0f;
            scoreMenu.SetActive(true);
        }
    }
}
