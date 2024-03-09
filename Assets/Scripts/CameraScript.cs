using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject targetObject;
    private Transform targetTransform;
    public List<Transform> backgroundTransform;
    public Transform transformPlatformManager;

    void Start()
    {
        targetObject = GameObject.FindWithTag("Player");

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
            Vector3 newBackgroundPosition = new Vector3(backgroundTransform[0].position.x, targetTransform.position.y, backgroundTransform[0].position.z);
            backgroundTransform[0].position = newBackgroundPosition;
        }
    }
}
