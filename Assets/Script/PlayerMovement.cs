using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animation; 
    private bool grounded;

    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    private void Update()
    {   
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player based on direction
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jump
        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Animation
        animation.SetBool("walk", horizontalInput != 0);
        animation.SetBool("grounded",grounded);
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
        public bool canAttack()
    {
        // Player can attack only when grounded and not jumping
        return grounded;
    }
}
