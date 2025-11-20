using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;      // Rigidbody2D reference
    private Vector2 movement;

    void Awake()
    {
        // Automatically find Rigidbody2D on this GameObject
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // Get horizontal and vertical input (WASD / Arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Optional: normalize diagonal movement so speed is consistent
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
