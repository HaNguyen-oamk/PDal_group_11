using UnityEngine;
using UnityEngine.EventSystems; // Required for IsPointerOverGameObject()

public class PlayerShooting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    SpriteRenderer sr; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Check if the game is currently running (Time.timeScale is > 0)
        if (Time.timeScale > 0f)
        {
            // 2. Check if the mouse is currently over a UI element (like the Pause Button)
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // If over UI, ignore the click for game actions.
                return;
            }

            // 3. Check for input to shoot
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (audioSource && shootSound)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set bullet direction based on player's flip state
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