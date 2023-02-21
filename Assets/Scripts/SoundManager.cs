using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(Explode))]
[RequireComponent(typeof(Respawn))]
public class SoundManager : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip respawn;
    public AudioClip explode;
    [SerializeField] private AudioSource soundPlayer1;
    [SerializeField] private AudioSource soundPlayer2;

    private void Awake()
    {
        SetJumpAnimations();
        SetExplodeAnimations();
        SetRespawnAnimations();
    }

    private void SetJumpAnimations()
    {
        var jump = GetComponent<Jump>();
        jump.OnJumpLeft += PlayJumpSound;
        jump.OnJumpRight += PlayJumpSound;
    }

    private void SetExplodeAnimations()
    {
        var explode = GetComponent<Explode>();
        explode.OnExplode += PlayExplodeSound;
    }

    private void SetRespawnAnimations()
    {
        var respawn = GetComponent<Respawn>();
        respawn.OnRespawn += PlayRespawnSound;
    }

    private void Play(AudioClip clip)
    {
        if (soundPlayer1.clip != null)
        {
            if(soundPlayer2.clip == clip)
            {
                return;
            }
            soundPlayer2.clip = clip;
            StartCoroutine(Playing(soundPlayer2));
        }

        if (soundPlayer1.clip == clip)
        {
            return;
        }

        soundPlayer1.clip = clip;
        StartCoroutine(Playing(soundPlayer1));
    }

    IEnumerator Playing(AudioSource soundPlayer)
    {
        soundPlayer.Play();
        yield return new WaitForSeconds(1.0f);
        soundPlayer.clip = null;
    }

    public void PlayJumpSound() { Play(jump); }
    public void PlayExplodeSound() { Play(explode); }
    public void PlayRespawnSound() { Play(respawn); }
}
