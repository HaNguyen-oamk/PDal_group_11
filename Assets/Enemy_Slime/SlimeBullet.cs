using UnityEngine;

public class SlimeBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 5f;        // Bullet travel speed
    public float lifeTime = 2f;     // Destroy after this time
    public int damage = 1;          // Damage per hit

    private Vector2 moveDirection;  // Direction bullet moves in

    void Start()
    {
        // Automatically destroy bullet after some time to avoid memory buildup
        Destroy(gameObject, lifeTime);
    }

    // Called from SlimeController to set bullet direction
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void Update()
    {
        // Move the bullet every frame
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        // Ignore collisions with the enemy who fired it
        if (hit.CompareTag("Enemy")) return;

        // If it hits the player
        if (hit.CompareTag("Player"))
        {
            Debug.Log("Slime bullet hit the player!");

            // Deal damage if the player has a health script
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Destroy bullet after hitting player
            Destroy(gameObject);
        }
        else
        {
            // Optionally, destroy bullet on wall or anything else
            Destroy(gameObject);
        }
    }
}
