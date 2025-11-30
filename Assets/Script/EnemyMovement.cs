using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public Transform player;      // Assign your player in Inspector
    public float speed = 3f;
    public float chaseRange = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseRange && grounded)
        {
            // Calculate direction to player
            float horizontalDirection = player.position.x - transform.position.x;

            // Move horizontally only
            rb.velocity = new Vector2(Mathf.Sign(horizontalDirection) * speed, rb.velocity.y);

            // Flip enemy based on movement direction
            if (rb.velocity.x > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (rb.velocity.x < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            // Walking/running animation
            animator.SetBool("run", Mathf.Abs(rb.velocity.x) > 0.01f);
        }
        else
        {
            // Stop moving when player is out of range
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("run", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if enemy is on the ground
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = false;
    }
}
