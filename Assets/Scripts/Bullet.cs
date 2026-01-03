using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private Vector2 dir = Vector2.up;

    public void SetDirection(Vector2 d)
    {
        if (d.sqrMagnitude < 0.01f) d = Vector2.up;
        dir = d.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GhostEnemy"))
        {
            Destroy(other.gameObject);   // kill ghost
            Destroy(gameObject);         // remove bullet
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);         // bullet hits wall
        }
    }
}
