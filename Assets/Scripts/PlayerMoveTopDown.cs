using System.Collections;
using UnityEngine;

public class PlayerMoveTopDown : MonoBehaviour
{
    public float speed = 5f;

    // Coins
    public CoinManager coinManager;

    // Shooting
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Health
    public int health = 100;
    public int enemyDamage = 25;
    public float hitCooldown = 0.5f; // prevents losing health every frame
    private bool canTakeHit = true;

    float x, y;
    float lastX = 0f, lastY = -1f;

    Animator anim;
    bool isMoving;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // Shoot
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(x, y, 0f);
        if (dir.magnitude > 1f) dir = dir.normalized;

        transform.Translate(dir * speed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;
        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SetDirection(new Vector2(lastX, lastY));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Coin
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            if (coinManager != null) coinManager.coinCount++;
        }

        // Enemy hit (ghost)
        if (other.CompareTag("GhostEnemy"))
        {
            TakeDamage(enemyDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // If ghost stays touching player, apply damage with cooldown
        if (other.CompareTag("GhostEnemy"))
        {
            TakeDamage(enemyDamage);
        }
    }

    private void TakeDamage(int dmg)
    {
        if (!canTakeHit) return;

        health -= dmg;
        Debug.Log("Player Health: " + health);

        StartCoroutine(FlashRed());
        StartCoroutine(HitCooldown());

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        if (spriteRenderer == null) yield break;

        Color original = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = original;
    }

    private IEnumerator HitCooldown()
    {
        canTakeHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canTakeHit = true;
    }

    private void Die()
    {
        // simplest: reload same scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
