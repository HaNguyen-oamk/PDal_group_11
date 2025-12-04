using UnityEngine;

public class SharkController : MonoBehaviour
{
    // Walking speed (default movement)
    public float walkSpeed = 3f;

    // Running speed (if needed)
    public float runSpeed = 6f;

    // Movement speed when using voice command (slower)
    public float voiceMoveSpeed = 1.5f;

    // Currently applied speed (used for non-voice movement)
    public float currentSpeed = 3f;

    // Voice input direction (set by the voice controller)
    public Vector2 voiceInput = Vector2.zero;

    // True when a voice command is active
    public bool voiceActive = false;

    // Speed used when voice command is active
    public float activeVoiceSpeed = 0f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    public Animator anim;

    bool isDead = false;
    bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Default speed is walking speed
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleSpaceAttack(); // Attack using Space key
    }

    void HandleMovement()
    {
        float x, y;

        // Default movement speed
        float speedToApply = currentSpeed;

        if (voiceActive)
        {
            // Use voice input
            x = voiceInput.x;
            y = voiceInput.y;

            // When voice command is active, use voice speed
            speedToApply = activeVoiceSpeed;
        }
        else
        {
            // Use keyboard input
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        // Calculate movement direction
        Vector2 dir = new Vector2(x, y).normalized;
        rb.velocity = dir * speedToApply;

        // Animation parameters
        anim.SetBool("isMoving", dir.magnitude > 0);
        anim.SetFloat("moveX", x);
        anim.SetFloat("moveY", y);

        // Flip sprite based on direction
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

    // Triggered by voice or keyboard
    public void TriggerAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        anim.SetBool("isAttacking", true);

        // End attack animation after 0.3 seconds
        Invoke(nameof(EndAttack), 0.3f);
    }

    void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    // Called by voice system when movement should stop
    public void StopMovement()
    {
        voiceInput = Vector2.zero;
        voiceActive = false;

        rb.velocity = Vector2.zero;

        // can reset currentSpeed back to walkSpeed here if necessary
    }
}
