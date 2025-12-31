using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    private float x;
    private float y;

    private bool pressingButton;
    private Animator anim;

    private bool pressingJumpButton;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");


        pressingButton = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        anim.SetBool("isRunning", pressingButton);


        pressingJumpButton = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);

        if (pressingJumpButton)
        {
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        if (pressingButton)
        {
            transform.Translate(new Vector3(x * speed * Time.fixedDeltaTime, 0f, 0f));

            if (x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        if (y > 0)
        {
            Vector3 jumpForce = new Vector3(0f, 7 * Time.fixedDeltaTime, 0f);
            transform.position += jumpForce;
        }


    }


}
