using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public Transform player;           // Assign your player in Inspector
    public PlayerHealth playerHealth;  // Assign PlayerHealth script (for hearts)
    public float speed = 3f;
    public float chaseRange = 5f;      // Distance to start chasing
    public float attackRange = 1.2f;   // Distance to trigger attack
    public float attackCooldown = 1f;  // Time between attacks

    private Rigidbody2D rb;
    private Animator animator;
    private bool grounded;
    private float lastAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Move towards player if in chase range but outside attack range
        if (distance < chaseRange && distance > attackRange && grounded)
        {
            float horizontalDirection = player.position.x - transform.position.x;
            rb.velocity = new Vector2(Mathf.Sign(horizontalDirection) * speed, rb.velocity.y);

            // Flip enemy based on movement direction
            transform.localScale = new Vector3(Mathf.Sign(horizontalDirection), 1, 1);

            animator.SetBool("run", true);
        }
        // Attack if within attack range
        else if (distance <= attackRange && grounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop moving
            animator.SetBool("run", false);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("EnemyAttack"); // Trigger attack animation
                lastAttackTime = Time.time;

                // Damage the player
                if (playerHealth != null)
                    playerHealth.TakeDamage(1);
            }
        }
        else
        {
            // Idle if player is out of range
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("run", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = false;
    }
}
