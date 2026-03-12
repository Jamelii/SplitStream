using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    bool isFacingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    [Header ("Gravity")]
    [SerializeField] private float baseGravity = 1f;
    [SerializeField] private float maxFallSpeed = 5f;
    [SerializeField] private float fallSpeedMultiplier = 2f;

    [Header("Wall Movement")]
    public float wallSlideSpeed = 2f;
    bool isWallSliding;

    void Start()
    {
        
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        GroundCheck();
        // WallCheck();
        ProcessGravity();
        Flip();
    }


    // Player Movement
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    // Player Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {


            if (context.performed)
            {
                // Regular Jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                // Short Hop
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
                jumpsRemaining--;
            }
        }
    }

    // Checks if player is touching the ground.
    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
    }

   /* private bool WallCheck()
    {
        return (Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer));
        
    } */

    // Controls the gravity that affects the player.
    public void ProcessGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; // Causes the player's fall speed to increase over time.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    // Controls the player's wall sliding.
     /*private void ProcessWallSlide()
    {
        if (!isGrounded & WallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        } 
        else
        {
            isWallSliding = false;
        }
    } */
    // Sprite flips based on player direction.
    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0) 
        {
            isFacingRight = !isFacingRight;
            Vector2 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visual Box to display ground check.
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);

        // Visual Box to display wall check.
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);
    }
}
