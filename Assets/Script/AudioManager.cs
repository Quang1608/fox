using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    public AudioClip bgm;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip cherryCollect;
    public AudioClip die;
    public AudioClip hurt;

    void Start()
    {
        BGMSource.clip = bgm;
        BGMSource.loop = true;
        BGMSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(0.9f, 1.1f);
        SFXSource.PlayOneShot(clip);
    }
}
