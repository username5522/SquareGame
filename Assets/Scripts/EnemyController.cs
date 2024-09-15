using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // L/R movement vars
    public float speed;
    private bool facingRight = true;

    // Rigidbody2D var
    private Rigidbody2D rb;

    // Ground Detection vars
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        if (!isGrounded)
        {
            Flip();
        }

        float horizontal = facingRight ? speed : -speed;
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
