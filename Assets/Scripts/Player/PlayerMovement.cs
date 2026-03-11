using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ForegroundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float wallJumpCooldown;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        // References for rigidbody and box collider
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip player depending on direction of movement (L/R)
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector2.one;
        } else if (horizontalInput < 0.01f){
            transform.localScale = new Vector2(-1, 1);
        }

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 7;

                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();
                }
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if(onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            rb.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.02f, ForegroundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.02f, wallLayer);
        return raycastHit.collider != null;
    }


}
