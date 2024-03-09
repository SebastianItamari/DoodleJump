using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public List<BackgroundGame> backgrounds;
    public Transform transformPlatformManager;
    public GameController gameController;
    private GameObject targetObject;
    private Transform targetTransform;
    private GameObject bg;
    private Transform bgTransform;
    private int index = 0;
    private bool change = false;

    void Start()
    {
        bg = (GameObject)Instantiate(backgrounds[index].background, new Vector2(0,0), Quaternion.identity);
        gameController.varY = backgrounds[index].varY;
        gameController.speedPlatforms = backgrounds[index].horizontalSpeeds;
        index++;
        targetObject = GameObject.FindWithTag("Player");

        bgTransform = bg.GetComponent<Transform>();

        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
            Debug.Log(targetTransform.localPosition);
        }
        else
        {
            Debug.LogError("Unable to find the external object with the specified tag.");
        }
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Mathf.RoundToInt(targetTransform.position.y) > 50 * index)
        {
            CreateNewBackground();
        }

        if (targetTransform.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, targetTransform.position.y,transform.position.z);
            transform.position = newPosition;
            Vector3 newBackgroundPosition = new Vector3(bgTransform.position.x, targetTransform.position.y, bgTransform.position.z);
            bgTransform.position = newBackgroundPosition;
        }
    }

    private void CreateNewBackground()
    {
        if (index < backgrounds.Count)
        {
            bg = Instantiate(backgrounds[index].background, new Vector2(0, targetTransform.position.y + 10), Quaternion.identity);
            gameController.varY = backgrounds[index].varY;
            gameController.speedPlatforms = backgrounds[index].horizontalSpeeds;

            bgTransform = bg.GetComponent<Transform>();
            index++;
        }
    }
}
