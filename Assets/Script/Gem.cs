using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public LoadGame GameOverScript;
    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.cherryCollect);
            GameOverScript.EndGame();
            Destroy(gameObject);
        }
    }
}
