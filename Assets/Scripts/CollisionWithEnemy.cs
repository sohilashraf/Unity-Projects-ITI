using Unity.VisualScripting;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemie"))
        {
            Debug.Log("Triggered by Enemy!");
        }
    }
    
   
}
