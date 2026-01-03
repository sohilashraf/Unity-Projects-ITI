using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);
    [SerializeField] private float smoothSpeed = 6f;

    [Header("Bounds")]
    public BoxCollider2D bounds;

    private float minX, maxX, minY, maxY;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        // Calculate camera limits from bounds
        Vector3 bMin = bounds.bounds.min;
        Vector3 bMax = bounds.bounds.max;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        minX = bMin.x + halfWidth;
        maxX = bMax.x - halfWidth;
        minY = bMin.y + halfHeight;
        maxY = bMax.y - halfHeight;
    }

    void LateUpdate()
    {
        Vector3 target = player.position + offset;

        float clampedX = Mathf.Clamp(target.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.y, minY, maxY);

        Vector3 targetPos = new Vector3(clampedX, clampedY, -10f);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
