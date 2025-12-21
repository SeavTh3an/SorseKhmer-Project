using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    [Header("Wave")]
    [Tooltip("Which wave this enemy belongs to")]
    public int waveNumber = 1;

    [Header("References")]
    public Transform player;
    public PlayerHealth playerHealth;

    [Header("Movement")]
    public float speed = 3f;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;

    [Header("Attack")]
    public float attackCooldown = 1f;

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
        // Safety checks
        if (player == null || WaveManager.Instance == null)
            return;

        // 🛑 Enemy only active in its own wave
        if (!WaveManager.Instance.IsWaveActive(waveNumber))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("run", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // 🏃 CHASE
        if (distance <= chaseRange && distance > attackRange && grounded)
        {
            float direction = player.position.x - transform.position.x;
            rb.velocity = new Vector2(Mathf.Sign(direction) * speed, rb.velocity.y);

            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
            animator.SetBool("run", true);
        }
        // ⚔ ATTACK
        else if (distance <= attackRange && grounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("run", false);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("EnemyAttack");
                lastAttackTime = Time.time;

                if (playerHealth != null)
                    playerHealth.TakeDamage(1);
            }
        }
        // 🧍 IDLE
        else
        {
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
