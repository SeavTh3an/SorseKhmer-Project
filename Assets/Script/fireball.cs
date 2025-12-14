using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        // Move in a straight line
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;

        // Only hit enemies
        if (collision.CompareTag("Enemy"))
        {
            hit = true;

            if (boxCollider != null)
                boxCollider.enabled = false;

            if (anim != null)
                anim.SetTrigger("explode");

            // Call enemy's hit method if exists
            collision.GetComponent<EnemyTyping>()?.OnHitByFireball();

            // Start coroutine to deactivate after animation
            StartCoroutine(DeactivateAfterAnimation());
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        hit = false;
        gameObject.SetActive(true);

        if (boxCollider != null)
            boxCollider.enabled = true;

        // Flip sprite if necessary
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    public void SetTarget(Transform target)
{
    if (target == null)
    {
        Debug.LogWarning("Projectile.SetTarget: target is null!");
        gameObject.SetActive(false);
        return;
    }

    Vector3 dirVector = (target.position - transform.position).normalized;
    direction = Mathf.Sign(dirVector.x); // Horizontal only

    hit = false;
    gameObject.SetActive(true);

    if (boxCollider != null)
        boxCollider.enabled = true;

    // Flip sprite if necessary
    float localScaleX = transform.localScale.x;
    if (Mathf.Sign(localScaleX) != direction)
        localScaleX = -localScaleX;

    transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
}


    private IEnumerator DeactivateAfterAnimation()
    {
        // Wait for explosion animation to finish (adjust time as needed)
        float animDuration = 0.5f;
        yield return new WaitForSeconds(animDuration);

        gameObject.SetActive(false);
    }
}
