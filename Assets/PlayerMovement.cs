using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float acceleration = 70f;     
    public float deceleration = 100f;  
    public float airControlMultiplier = 0.7f; 

    [Header("Jumping")]
    public float jumpHeight = 5f;
    public float maxJumpHeight = 8f;
    public float maxFallSpeed = -20f;
    public float fallMultiplier = 4f;     
    public float lowJumpMultiplier = 5f;  

    [Header("Coyote Time & Jump Buffering")]
    public float coyoteTime = 0.2f; 
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;   
    private float jumpBufferCounter;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    [Header("Sprite & Flip")]
    private bool facingRight = true;  
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float velocityXSmoothing;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && !isJumping)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

       
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }

        
        HandleSpriteFlip();
    }

    void FixedUpdate()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        float targetSpeed = horizontalInput * moveSpeed;
        float accelerationRate = isGrounded ? acceleration : acceleration * airControlMultiplier;
        float decelerationRate = isGrounded ? deceleration : deceleration * airControlMultiplier;

        
        if (horizontalInput != 0)
        {
            rb.velocity = new Vector2(
                Mathf.SmoothDamp(rb.velocity.x, targetSpeed, ref velocityXSmoothing, 1f / accelerationRate),
                rb.velocity.y
            );
        }
        else
        {
            rb.velocity = new Vector2(
                Mathf.SmoothDamp(rb.velocity.x, 0, ref velocityXSmoothing, 1f / decelerationRate),
                rb.velocity.y
            );
        }
    }

    void Jump()
    {
        isJumping = true;
        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

        coyoteTimeCounter = 0f;
        jumpBufferCounter = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
        }
    }

    void HandleSpriteFlip()
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
