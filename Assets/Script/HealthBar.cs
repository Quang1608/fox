using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public LoadGame GameOverScript;
    public AudioManager audioManager;
    public float health = 100f;

    public void loseHP(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        fillBar.fillAmount = health / 100f;

        if (health < 100 && health > 0 && amount > 0)
        {
            audioManager.PlaySFX(audioManager.hurt);
        }

        if (health <= 0) Die();
    }

    public void Die()
    {
        audioManager.PlaySFX(audioManager.die);
        audioManager.BGMSource.Stop();
        Destroy(gameObject);
        GameOverScript.EndGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            loseHP(10f);
        }
    }
}
