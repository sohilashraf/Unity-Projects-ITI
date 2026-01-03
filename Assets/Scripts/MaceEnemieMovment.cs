using UnityEngine;

public class MaceEnemieMovment : MonoBehaviour
{
    public AnimationCurve movementCurve;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, movementCurve.Evaluate(Time.time % (movementCurve.length)), transform.position.z);
    }
}
