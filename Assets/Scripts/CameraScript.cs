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

    void Start()
    {
        bg = (GameObject)Instantiate(backgrounds[0].background, new Vector2(0,0), Quaternion.identity);
        gameController.varY = backgrounds[0].varY;
        gameController.speedPlatforms = backgrounds[0].horizontalSpeeds;
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
        if(targetTransform.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, targetTransform.position.y,transform.position.z);
            transform.position = newPosition;
            Vector3 newBackgroundPosition = new Vector3(bgTransform.position.x, targetTransform.position.y, bgTransform.position.z);
            bgTransform.position = newBackgroundPosition;
        }
    }
}
