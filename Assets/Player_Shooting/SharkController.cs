using UnityEngine;

public class SharkController : MonoBehaviour
{
    public float speed = 3f;
    public Animator anim;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool isDead = false;
    bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;     
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y).normalized;
        rb.velocity = dir * speed;

        anim.SetBool("isMoving", dir.magnitude > 0);
        anim.SetFloat("moveX", x);
        anim.SetFloat("moveY", y);

        if (x < 0) sr.flipX = true;
        if (x > 0) sr.flipX = false;
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }
    }

    public void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        rb.velocity = Vector2.zero;
    }
}
