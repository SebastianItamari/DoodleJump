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
    private GameObject plat;
    private float lastY = 6.14f;
    private float varY = 3f;
    private float score = 0f;
    void Start()
    {
      
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
            int aux = Random.Range(1, 10);
            if (aux == 1)
            {
                //y + 7.5, y + 11
                plat = (GameObject)Instantiate(platforms[2], new Vector2(Random.Range(-7.11f, 7.11f), lastY + varY), Quaternion.identity);
            }
            else if (aux > 1 && aux < 4)
            {
                plat = (GameObject)Instantiate(platforms[1], new Vector2(Random.Range(-7.11f, 7.11f), lastY + varY), Quaternion.identity);
            }
            else
            {
                plat = (GameObject)Instantiate(platforms[0], new Vector2(Random.Range(-7.11f, 7.11f), lastY + varY), Quaternion.identity);
            }

            lastY = plat.transform.position.y;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            Time.timeScale = 0f;
            scoreMenu.SetActive(true);
        }
    }
}
