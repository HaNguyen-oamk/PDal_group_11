using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    SpriteRenderer sr;    // 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (audioSource && shootSound)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

       
        if (sr.flipX)
        {
            rb.velocity = Vector2.left * bulletSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * bulletSpeed;
        }
    }
}
