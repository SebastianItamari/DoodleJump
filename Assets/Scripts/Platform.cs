using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jump;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.relativeVelocity.y <= 0)
            {
                Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    Vector2 velocity = rigidbody.velocity;
                    velocity.y = jump;
                    rigidbody.velocity = velocity;
                }
            }
        }
    }
}
