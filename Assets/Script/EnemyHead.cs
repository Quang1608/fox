using Unity.VisualScripting;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb.linearVelocityY > -0.1) return;

            enemy.Die();
        }
    }
}
