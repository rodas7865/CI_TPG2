using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Player speed (can be set in the Inspector)
    public float speed = 5f;

    // --- NEW: Boundary limits for the X axis ---
    // You can change these numbers in the Unity Inspector!
    public float minX = -8f; 
    public float maxX = 8f;

    // The Rigidbody2D component
    private Rigidbody2D rb;

    // Movement we’ll apply
    private Vector2 move;

    // Called at the start
    void Start()
    {
        // Get the player's Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    // Called every physics step (default 0.02 sec)
    void FixedUpdate()
    {
        float x = 0f;
        
        // Get keyboard input (left (x < 0) or right (x > 0))
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
                x = -1f;
            else if (Keyboard.current.rightArrowKey.isPressed)
                x = 1f;
        }

        // Store movement vector, keep falling speed physics-driven
        move = new Vector2(x * speed, rb.linearVelocity.y);

        // Apply movement to the player
        rb.linearVelocity = move;

        // --- NEW: Limit the player's X position ---
        // Grab the current position
        Vector2 clampedPosition = transform.position;
        
        // Force the X value to stay between minX and maxX
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        
        // Apply the newly clamped position back to the player
        transform.position = clampedPosition;
    }
}