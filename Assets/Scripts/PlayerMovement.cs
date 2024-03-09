using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D bodyPLalyer;
    private BoxCollider2D colliderPlayer;
    private bool right = true;
    private float move = 0;
    private Animator animatorPlayer;
    private Vector2 refSpeed = Vector2.zero;
    private float leftLimit = -7.998f;
    private float rightLimit = 7.998f;
    private bool up = false;
   
    void Start()
    {
        bodyPLalyer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        colliderPlayer = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        if (bodyPLalyer.velocity.y < 0 && up == true)
        {
            colliderPlayer.offset = new Vector2(colliderPlayer.offset.x, 0.7475104f);
            animatorPlayer.SetBool("Up", false);
            up = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVel = new Vector2(move * speed, bodyPLalyer.velocity.y);
        bodyPLalyer.velocity = Vector2.SmoothDamp(bodyPLalyer.velocity, targetVel, ref refSpeed,0.1f);
        //bodyPLalyer.velocity = targetVel;

        if(move > 0 && !right)
        {
            ChangeHorizontalDirection();
        }
        else if(move < 0 && right)
        {
            ChangeHorizontalDirection();
        }
        if (transform.position.x > rightLimit)
        {
            transform.position = new Vector2(leftLimit, transform.position.y);
        }
        else if (transform.position.x < leftLimit)
        {
            transform.position = new Vector2(rightLimit, transform.position.y);
        }
    }

    private void ChangeHorizontalDirection()
    {
        right = !right;
        Vector2 aux = transform.localScale;
        aux.x *= -1;
        transform.localScale = aux;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            colliderPlayer.offset = new Vector2(colliderPlayer.offset.x, 1.68f);
            animatorPlayer.SetBool("Up",true);
            up = true;
        }
    }
}
