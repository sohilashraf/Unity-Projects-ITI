using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    public Collider2D feetCollider;   
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private float x;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        // Ground check using feet collider
        isGrounded = feetCollider.IsTouchingLayers(groundLayer);

        // Animations
        anim.SetBool("isRunning", x != 0);

        // Flip
        if (x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
    }
}
