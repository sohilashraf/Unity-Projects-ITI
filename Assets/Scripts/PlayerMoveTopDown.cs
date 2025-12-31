using UnityEngine;

public class PlayerMoveTopDown : MonoBehaviour
{
    public float speed = 5f;

    float x, y;
    float lastX = 0f, lastY = -1f; // default facing down

    Animator anim;
    bool isMoving;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        isMoving = (x != 0 || y != 0);

        if (isMoving)
        {
            lastX = x;
            lastY = y;
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("moveX", lastX);
        anim.SetFloat("moveY", lastY);
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(x, y, 0f);
        if (dir.magnitude > 1f)
            dir = dir.normalized;

        transform.Translate(dir * speed * Time.fixedDeltaTime);
    }
}
