using UnityEngine;

public class SharkController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float currentSpeed = 3f;

    public Vector2 voiceInput = Vector2.zero;
    public bool voiceActive = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator anim;

    bool isDead = false;
    bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleSpaceAttack(); // attack by space key board
    }

    void HandleMovement()
    {
        float x, y;

        if (voiceActive)
        {
            x = voiceInput.x;
            y = voiceInput.y;
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        Vector2 dir = new Vector2(x, y).normalized;
        rb.velocity = dir * currentSpeed;

        anim.SetBool("isMoving", dir.magnitude > 0);
        anim.SetFloat("moveX", x);
        anim.SetFloat("moveY", y);

        if (x < 0) sr.flipX = true;
        if (x > 0) sr.flipX = false;
    }

    void HandleSpaceAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerAttack();
        }
    }

    // voice will call function
    public void TriggerAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        anim.SetBool("isAttacking", true);

        // Hủy sau 0.3 giây
        Invoke(nameof(EndAttack), 0.3f);
    }

    void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    public void StopMovement()
    {
        voiceInput = Vector2.zero;
        voiceActive = false;
        rb.velocity = Vector2.zero;
    }
}
