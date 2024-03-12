using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameController gameController;
    [SerializeField] private StartPlayer startPlayer;
    private GameObject targetObject;
    private Transform targetTransform;
    private GameObject bg;
    private Transform bgTransform;
    private int index = 0;
    private int spaceBetweenLevels = 50;

    void Start()
    {
        bg = (GameObject)Instantiate(levels[index].background, new Vector2(0,0), Quaternion.identity);
        gameController.varY = levels[index].varY;
        gameController.speedPlatforms = levels[index].horizontalSpeeds;
        gameController.recurrenceCoin = levels[index].recurrenceCoin;
        index++;

        targetObject = startPlayer.player;
        targetTransform = targetObject.transform;

        bgTransform = bg.GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (Mathf.RoundToInt(targetTransform.position.y) > spaceBetweenLevels * index)
        {
            CreateNewBackground();
        }

        if (targetTransform.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, targetTransform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
            if(targetTransform.position.y > bgTransform.position.y)
            {
                Vector3 newBackgroundPosition = new Vector3(bgTransform.position.x, targetTransform.position.y, bgTransform.position.z);
                bgTransform.position = Vector3.Lerp(bgTransform.position, newBackgroundPosition, 1f);
            }           
        }
    }

    private void CreateNewBackground()
    {
        if (index < levels.Count)
        {
            bg = Instantiate(levels[index].background, new Vector2(0, targetTransform.position.y + 9.8f), Quaternion.identity);
            gameController.varY = levels[index].varY;
            gameController.speedPlatforms = levels[index].horizontalSpeeds;

            bgTransform = bg.GetComponent<Transform>();
            index++;
        }
    }
}
