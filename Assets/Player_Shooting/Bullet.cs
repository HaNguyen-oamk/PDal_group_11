using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 0.5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // destroy in few sec
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            // call function die slime
            SlimeController slime = hit.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.TakeDamage();  // Slime die
            }

            Debug.Log("Player hit: " + hit.name); //debug test

            Destroy(gameObject); // destroy bullet
        }
    }
}
