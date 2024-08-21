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
    public bool isGrounded;


    [Header("Sprite & Flip")]
    private bool facingRight = true;


    [Header("Animation")]
    public Animator animator;
    private string currentState;
    const string IDLE = "Idle";
    const string RUN = "Run";
    const string JUMP = "Jump";
    const string FALL = "Fall";

    public Rigidbody2D rb;
    public float horizontalInput;
    private float velocityXSmoothing;
    private bool isJumping;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
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

      
        if (rb.velocity.y > 0 && !isGrounded)
        {
            
            ChangeAnimationState(JUMP);
        }
        else if (rb.velocity.y < 0 && !isGrounded)
        {
            //animator.SetBool("IsFalling", true);
            //animator.SetBool("IsJumping", false);
            ChangeAnimationState(FALL);
        }
        else if (horizontalInput != 0 && isGrounded)
        {
            ChangeAnimationState(RUN);
        }
        else if (horizontalInput == 0 && isGrounded)
        {
            ChangeAnimationState(IDLE);
        }
        //else
        //{
        //    animator.SetBool("IsJumping", false);
        //    animator.SetBool("IsFalling", false);
        //}


        horizontalInput = Input.GetAxisRaw("Horizontal");

       
        //animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

       
        HandleSpriteFlip();

       
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= dashCooldownTime)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

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

        //if(horizontalInput > 0 || horizontalInput < 0)
        //{
        //    ChangeAnimationState(RUN);
        //}
        //else if(horizontalInput == 0 && isGrounded) 
        //{
        //    ChangeAnimationState(IDLE);
        //}
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTime = Time.time + dashCooldown;
        rb.velocity = new Vector2(facingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        //animator.SetTrigger("Dash"); 
    }

    void EndDash()
    {
        isDashing = false;
    }

    void Jump()
    {
        isJumping = true;
        ChangeAnimationState(JUMP);
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

    void ChangeAnimationState(string newState)
    {

        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
