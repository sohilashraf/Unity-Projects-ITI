using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;
    public float jumpForce = 7f;

    public Collider2D feetCollider;   
    public LayerMask groundLayer;

    public CoinManager coinManager;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator anim;
    private float x;
    private bool isGrounded;
    private bool dyingInWater = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinManager.coinCount++;
        }
        if (other.gameObject.CompareTag("Enemie"))
        {
            Die();
            Debug.Log("Player Died!");

        }

        if (other.CompareTag("Water") && !dyingInWater)
        {
            dyingInWater = true;
            StartCoroutine(DieAfterWater());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("Player Health: " + health);
            StartCoroutine(FlashRed());

            if (health <= 0)
            {
                Debug.Log("Player Died!");
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Enemie"))
        {
           Die();
           Debug.Log("Player Died!");

        }
    }
      private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2DScene");
    }
    private IEnumerator DieAfterWater()
    {
        yield return new WaitForSeconds(6f);
        Die();
    }

}
