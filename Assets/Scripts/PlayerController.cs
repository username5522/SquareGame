using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float moveInput;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private int extraJumpsValue;
    [SerializeField] private float jumpTime;
    private int extraJumps;
    private bool isJumping;
    private bool isGrounded;
    private float jumpTimeCounter;

    [Header("Fall Multiplier")]
    [SerializeField] private float fallMultiplier = 30f;
    [SerializeField] float lowJumpMultiplier = 27.5f;

    [Header("Speed Multiplier")]
    [SerializeField] private float speedMultiplierTime = .25f;
    [SerializeField] private float speedMultCooldown = .25f;
    private float speedMultiplier = 1f;
    private float speedMultCooldownCounter = 0f;
    private float speedMultiplierCounter = 0f;
    private bool isDashing = false;
    private bool isCooldownActive = false;

    private Rigidbody2D rb;

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJump();
        HandleFallMultiplier();
        HandleDashing();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            checkRadius,
            LayerMask.GetMask("Ground")
        );
    }

    void HandleMovement()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed * speedMultiplier, rb.velocity.y);
    }

    void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        speedMultiplier = 1f;
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (extraJumps > 0)
            {
                PerformJump();
                extraJumps--;
            }
            if (isGrounded == true && extraJumps == 0)
            {
                PerformJump();
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping && jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = false;
            speedMultiplier = 1f;
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            isJumping = false;
        }
    }

    void HandleFallMultiplier()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.y < 0)
                rb.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
                rb.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
    }

    void HandleDashing()
    {
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) &&
            Input.GetButton("Horizontal") && isGrounded && !isDashing &&
            !isCooldownActive)
        {
            speedMultiplierCounter = speedMultiplierTime;
            speedMultiplier = 1.5f;
            isDashing = true;
            isCooldownActive = false;
        }

        if (isDashing)
        {
            speedMultiplierCounter -= Time.deltaTime;
            if (speedMultiplierCounter <= 0)
            {
                isDashing = false;
                isCooldownActive = true;
                speedMultCooldownCounter = speedMultCooldown;
            }
        }

        if (isCooldownActive)
        {
            speedMultCooldownCounter -= Time.deltaTime;
            if (speedMultCooldownCounter <= 0)
            {
                isCooldownActive = false;
                speedMultiplier = 1f;
            }
        }

        if (speedMultiplierCounter > 0) speedMultiplierCounter -= Time.deltaTime;
        else speedMultiplier = 1f;
    }

}