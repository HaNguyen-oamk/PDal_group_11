using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    [Header("Movement Settings")]

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip shootSound;

    public float moveSpeed = 2f;
    public float moveRange = 4f;

    [Header("Attack Settings")]
    public GameObject bulletPrefab;   // Prefab of the projectile
    public Transform firePoint;       // Where bullets spawn (mouth position)
    public float attackInterval = 10f; // How often to attack time between 2 times shooting

    private Animator anim;
    private Vector2 targetPos;
    private bool isDead = false;
    private Vector2 startPos;

    void Start()
    {
        // Cache the Animator
        anim = GetComponent<Animator>();

         // 
        startPos = transform.position;

        // Start the movement and attack loops
        StartCoroutine(RandomMove());
        StartCoroutine(AutoAttack());
    }

    void Update()
    {
        if (isDead) return;
        MoveToTarget();
    }

    //
    // Random wandering movement
    //
    IEnumerator RandomMove()
    {
        while (!isDead)
        {
            // position randome around (startPos)
            float offsetX = Random.Range(-moveRange, moveRange);
            float offsetY = Random.Range(-moveRange / 2f, moveRange / 2f);
            targetPos = startPos + new Vector2(offsetX, offsetY);

            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }


    void MoveToTarget()
    {
        // Move smoothly toward the target position
        if (Vector2.Distance(transform.position, targetPos) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Flip slime to face direction of movement
            transform.localScale = new Vector3(targetPos.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    // 
    // Automatic periodic attacks
    // 
    IEnumerator AutoAttack()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(attackInterval);

            // Trigger attack animation
            anim.SetTrigger("attack");

            // Small delay before shooting (syncs with animation)
            yield return new WaitForSeconds(0.3f);

            FireWeapon();
        }
    }

    //
    // Shoot 
    //
   void FireWeapon()
{
    if (bulletPrefab == null || firePoint == null) return;
    
    if (audioSource && shootSound)
        audioSource.PlayOneShot(shootSound);

    GameObject player = GameObject.FindGameObjectWithTag("Player"); //change tag later now tag is gun
    if (player == null) return;

    // genneral bullet
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

    // calculator bullet from slime -> player
    Vector2 dir = (player.transform.position - firePoint.position).normalized;

    // sent direction script bullet
    SlimeBullet bulletScript = bullet.GetComponent<SlimeBullet>();
    if (bulletScript != null)
        bulletScript.SetDirection(dir);

    Debug.Log("Slime fired bullet toward player");
}


    // 
    //  Death handling later ()****
    //
    public void TakeDamage()
    {
        if (isDead) return;

        isDead = true;
        anim.SetBool("isDead", true);

        StopAllCoroutines();
        Destroy(gameObject, 1f);
    }

    //slime have trigger to player minus one blood
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);   // minus 1 blood
            }
        }
    }
}
