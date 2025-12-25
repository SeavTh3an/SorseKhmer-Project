using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animation; 
    private bool grounded;

    // Add this for typing mode
    public bool canMove = true;

    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    private void Update()
{
    if (!canMove)
    {
        body.velocity = Vector2.zero;
        animation.SetBool("walk", false);
        return;
    }

    float horizontalInput = Input.GetAxis("Horizontal");
    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

    if (horizontalInput > 0.01f)
        transform.localScale = Vector3.one;
    else if (horizontalInput < -0.01f)
        transform.localScale = new Vector3(-1, 1, 1);

    if (Input.GetKey(KeyCode.Space) && grounded)
        Jump();

    animation.SetBool("walk", horizontalInput != 0);
    animation.SetBool("grounded", grounded);
}


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animation.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }

    public bool CanAttack()
    {
        // Player can attack only when grounded
        return grounded;
    }
}
