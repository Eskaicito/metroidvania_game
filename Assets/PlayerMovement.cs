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
    public float maxJumpTime = 0.35f; // Tiempo máximo que puedes mantener el salto
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public float airControlFactor = 0.5f; // Control en el aire

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
    private float jumpTimeCounter;

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
            jumpTimeCounter = maxJumpTime; // Comienza a contar el tiempo de salto
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        if (Input.GetButton("Jump") && isJumping && jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
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
    }

    private void MovePlayer()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;

        // Ajuste para evitar que el jugador cambie bruscamente de dirección en el aire
        if (!IsGrounded() && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            // Si está en el aire, solo permite un cambio gradual de dirección
            targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, Time.fixedDeltaTime * 2f);
        }

        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * acceleration, velocityPower) * Mathf.Sign(speedDiff);
        rb.AddForce(movement * Vector2.right);

        // Ajuste para control de movimiento en el aire
        if (isJumping)
        {
            float airControl = Mathf.Lerp(1f, airControlFactor, 1f - (jumpTimeCounter / maxJumpTime));
            rb.velocity = new Vector2(horizontalInput * moveSpeed * airControl, rb.velocity.y);
        }

        if (Mathf.Abs(horizontalInput) < 0.01f)
        {
            float friction = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            friction *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -friction, ForceMode2D.Impulse);
        }
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
