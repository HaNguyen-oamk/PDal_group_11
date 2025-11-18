using UnityEngine;

public class SlimeBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 1f;
    public float damage = 1f; // damage lose 25% blood

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        // if collide Player → damage
        if (hit.CompareTag("Player"))
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        // if collide wall or others-> destroy
        if (!hit.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
