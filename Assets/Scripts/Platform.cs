using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jump;
    public float horizontalSpeed;
    [SerializeField] private AudioClip platformSound;
    private float leftLimit = -7.11f;
    private float rightLimit = 7.11f;
    private Rigidbody2D platform;
    private bool movingRight = true;

    void Start()
    {
        platform = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float speedX;
        if (movingRight)
        {
            speedX = horizontalSpeed;
        }
        else
        {
            speedX = -horizontalSpeed;
        }

        platform.velocity = new Vector2(speedX, platform.velocity.y);

        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Verificar si el punto de contacto está en la parte inferior de la plataforma
                if (IsContactPointBelow(collision.gameObject, contact.point))
                {
                    Rigidbody2D playerRigidbody = collision.collider.GetComponent<Rigidbody2D>();
                    if (playerRigidbody != null)
                    {
                        AudioController.instance.Reproduce(platformSound);
                        Vector2 velocity = playerRigidbody.velocity;
                        velocity.y = jump;
                        playerRigidbody.velocity = velocity;
                    }
                    break; // Salir del bucle si encontramos un punto de contacto inferior
                }
            }
        }
    }

    // Método para verificar si el punto de contacto está debajo del jugador
    private bool IsContactPointBelow(GameObject player, Vector2 contactPoint)
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            // Calcular el centro inferior del colisionador del jugador
            Vector2 bottomCenter = playerCollider.bounds.center - new Vector3(0, playerCollider.bounds.extents.y, 0);
            // Verificar si el punto de contacto está por debajo del centro inferior del colisionador del jugador
            return contactPoint.y < bottomCenter.y;
        }
        return false;
    }
}
