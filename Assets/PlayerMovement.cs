using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float velocityPower = 0.9f;
    public float frictionAmount = 0.3f;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animations")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool isFacingRight = true;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        
        if (IsGrounded())
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

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            isJumping = true;
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        // Crouching logic
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (isJumping)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * acceleration, velocityPower) * Mathf.Sign(speedDiff);

        rb.AddForce(movement * Vector2.right);

        if (Mathf.Abs(horizontalInput) < 0.01f)
        {
            float friction = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            friction *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -friction, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void UpdateAnimations()
    {
        if (isCrouching)
        {
            animator.Play("Crouch");
        }
        else if (!IsGrounded())
        {
            animator.Play("Jump");
        }
        else if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle");
        }

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
