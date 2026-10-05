using Unity.VisualScripting;
using UnityEngine;
public class EnemyBody : MonoBehaviour
{
    [SerializeField] float bounceForce = 1f;
    [SerializeField] float hitInterval = 0.5f;

    private float currentTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentTime < hitInterval) return;

            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb == null) return;

            HealthBar healthBar = other.GetComponent<HealthBar>();
            healthBar.loseHP(25f);
            currentTime = 0f;
            
            playerRb.AddForce(new Vector2(0, bounceForce));
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
    }
}
