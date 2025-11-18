using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    public GameObject bulletPrefab;   // Prefab 
    public Transform firePoint;       // start position shoot
    public float bulletSpeed = 10f;

    void Update()
    {
        // shoot by key space or mouse
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (audioSource && shootSound)
            audioSource.PlayOneShot(shootSound);

        // genneral bullet in firePoint pos
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // take Rigidbody2D of bullet to move
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // direction right of firePoint

    }
}
