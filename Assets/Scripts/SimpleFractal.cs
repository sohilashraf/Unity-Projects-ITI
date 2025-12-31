using UnityEngine;

public class SimpleFractal : MonoBehaviour
{
    [Range(1, 5)] // SAFETY LIMIT (prevents Unity crash)
    [SerializeField] private int cycleCount = 1;

    [SerializeField] private float scaleRatio = 0.3f;

    [SerializeField] private Color startTint = Color.black;
    [SerializeField] private Color endTint = Color.red;

    [SerializeField] private bool alternateForms = true;
    [SerializeField] private bool refreshLive = true;

    private int lastCycleCount;

    void Start()
    {
        Build();
    }

    void Update()
    {
        if (!refreshLive) return;

        if (cycleCount != lastCycleCount)
        {
            Build();
        }
    }

    void Build()
    {
        ClearHierarchy();
        lastCycleCount = cycleCount;

        SpawnNode(cycleCount, transform, Vector3.zero, 1f);
    }

    void ClearHierarchy()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void SpawnNode(int level, Transform parent, Vector3 position, float scale)
    {
        if (level <= 0) return;

        PrimitiveType shapeType = PrimitiveType.Cube;

        if (alternateForms && level % 2 == 1)
            shapeType = PrimitiveType.Sphere;

        GameObject node = GameObject.CreatePrimitive(shapeType);
        node.transform.SetParent(parent, false);
        node.transform.localPosition = position;
        node.transform.localScale = Vector3.one * scale;

        ApplyGradient(node, level);

        Vector3[] axes =
        {
            Vector3.up,
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back
        };

        float spacing = 0.5f + (0.5f * scaleRatio);

        foreach (Vector3 axis in axes)
        {
            SpawnNode(
                level - 1,
                node.transform,
                axis * spacing,
                scaleRatio
            );
        }
    }

    void ApplyGradient(GameObject obj, int level)
    {
        float blend =
            cycleCount > 1
            ? (float)(cycleCount - level) / (cycleCount - 1)
            : 0f;

        obj.GetComponent<Renderer>().material.color =
            Color.Lerp(startTint, endTint, blend);
    }
}
