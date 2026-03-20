using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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
    public float wallSlideSpeed = 2;
    bool isWallSliding;

    [Header("Wall Jumping")]
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.3f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 20f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();
        WallCheck();
        ProcessGravity();
        ProcessWallSlide();
        ProcessWallJump();

        // If player isn't wall jummping and changes directions, call the Flip method.
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
            Flip();
        }
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

        // Wall Jumping

        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); // Makes the player Jump away from the wall.
            wallJumpTimer = 0f;

            // Force the player to flip when wall jumping.
            if(transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector2 ls = transform.localScale;
                ls.x *= -1;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); // Wall jump lasts 0.3 seconds -- Can jump again after 0.4 seconds (Keeps movement feeling more fluid).
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

    // Checks if the player is touching a wall.
    private bool WallCheck()
    {
        return (Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer));
    }

    // Controls the gravity that affects the player.
    public void ProcessGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; // Causes the player's fall speed to increase over time.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); // Caps the player's fall rate
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    // Controls the player's wall sliding.
    private void ProcessWallSlide()
    {
        if (!isGrounded & WallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed)); // Caps off the player's wall sliding rate.
        } 
        else
        {
            isWallSliding = false;
        }
    } 

    // Controls the player's Wall Jump
    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if(wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    } 

    // Cancels the player's wall jump
    private void CancelWallJump()
    {
        isWallJumping = false;
    }
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

    // Draws boundary boxes for viewing in editor to see colliders
    private void OnDrawGizmosSelected()
    {
        // Visual Box to display ground check.
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);

        // Visual Box to display wall check.
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);
    }

    // Checks if the player has touched any traps.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "Pit")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
}
