using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private AudioClip coin;
    private Score score;
    void Start()
    {
        GameObject scoreObject = GameObject.Find("Score");

        if (scoreObject != null)
        {
            score = scoreObject.GetComponent<Score>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            AudioController.instance.Reproduce(coin);
            score.score += points;
            score.additionalScore += points;
            Destroy(gameObject);
        }
    }
}
