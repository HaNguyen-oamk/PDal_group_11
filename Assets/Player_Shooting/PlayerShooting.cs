using UnityEngine;
using UnityEngine.EventSystems; // Required for IsPointerOverGameObject()

public class PlayerShooting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    
    // tag of enemy
    public string enemyTag = "Enemy"; 
    // **Max distance find **(change if want)
    public float searchRadius = 20f; 

    SpriteRenderer sr; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale > 0f)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    // Find nearest enemy
    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy)
                continue;

            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

            if (distanceToEnemy < shortestDistance && distanceToEnemy <= searchRadius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    public void Shoot()
    {
        if (audioSource && shootSound)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet;
        Rigidbody2D rb;

        //  **Find nearest enemy
        Transform target = FindNearestEnemy();

        if (target == null)
        {
            Debug.Log("No enemy found. Shooting forward.");
            
            Vector2 defaultDirection = sr.flipX ? Vector2.left : Vector2.right;

            bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = defaultDirection * bulletSpeed;

            return; 
        }
        
    
        Vector3 directionToTarget = (target.position - firePoint.position).normalized;
        
        //update char direction
        if (directionToTarget.x < 0)
        {
            sr.flipX = true;
        }
        else if (directionToTarget.x > 0)
        {
            sr.flipX = false;
        }

        // cal angle
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // 
        bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        rb = bullet.GetComponent<Rigidbody2D>();

        // apply velocity cal direction
        rb.velocity = directionToTarget * bulletSpeed;
    }
}
