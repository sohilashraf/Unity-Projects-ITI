using UnityEngine;

public class LadderMovment : MonoBehaviour
{
    private float vertical;
    private float climbSpeed = 5f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (isLadder == true && Mathf.Abs(vertical)> 0f) 
        {
            isClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
