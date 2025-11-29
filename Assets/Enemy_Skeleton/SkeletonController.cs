using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float chaseRange = 5f;

    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float attackCooldown = 5f;
    private float attackTimer = 0f;

    [Header("Health Settings")]
    public int maxHealth = 1;
    private int currentHealth;
    private bool isDead = false;

    private Animator anim;
    private Collider2D col;
    private Transform playerTarget;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        currentHealth = maxHealth;

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            playerTarget = obj.transform;
    }

    void Update()
    {
        if (isDead) return;
        if (playerTarget == null) return;

        // Movement Logic
        float distance = Vector2.Distance(transform.position, playerTarget.position);
        bool isMoving = distance > 1.2f;  // Stop near player

        anim.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            MoveTowardPlayer();
        }

        // Attack Logic
        attackTimer += Time.deltaTime;

        if (distance <= 1.4f && attackTimer >= attackCooldown)
        {
            TriggerAttack();
            attackTimer = 0f;
        }
    }

    void MoveTowardPlayer()
{
    Vector2 dir = (playerTarget.position - transform.position).normalized;

    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);

    // Flip sprite
    if (dir.x < 0)
        transform.localScale = new Vector3(-1, 1, 1);
    else
        transform.localScale = new Vector3(1, 1, 1);
}

    // Called when Skeleton attacks (Animator trigger)
    public void TriggerAttack()
    {
        anim.SetTrigger("attack");  // both attack animations will run
    }

    // Called from Animation Event
    public void DealDamage()
    {
        if (playerTarget == null || isDead) return;

        PlayerHealth player = playerTarget.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("Skeleton hits player!");
            player.TakeDamage(attackDamage);
        }
    }

    // Called when player damages skeleton
    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        anim.SetTrigger("isDead");

        if (col != null)
            col.enabled = false;

        // Stop movement/logic
        this.enabled = false;

        Destroy(gameObject, 2f);  // wait for animation to finish
    }
}
