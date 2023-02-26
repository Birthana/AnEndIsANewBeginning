using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(Explode))]
[RequireComponent(typeof(Respawn))]
public class AnimationManager : MonoBehaviour
{
    public List<AnimationClip> clips = new List<AnimationClip>();

    private AnimationClip IDLE;
    private AnimationClip MOVE_LEFT;
    private AnimationClip MOVE_RIGHT;
    private AnimationClip JUMP_LEFT;
    private AnimationClip JUMP_RIGHT;
    private AnimationClip EXPLODE;
    private AnimationClip RESPAWN;

    private Animator anim;
    private AnimationClip currentClip;
    private bool IsInterrupted;
    private Coroutine currentCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        SetAnimationHash();
        SetMoveAnimations();
        SetJumpAnimations();
        SetExplodeAnimations();
        SetRespawnAnimations();
    }

    private void SetAnimationHash()
    {
        IDLE = clips[0];
        MOVE_LEFT = clips[1];
        MOVE_RIGHT = clips[2];
        JUMP_LEFT = clips[3];
        JUMP_RIGHT = clips[4];
        EXPLODE = clips[5];
        RESPAWN = clips[6];
    }

    private void SetMoveAnimations()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnLeft += PlayLeft;
        playerMovement.OnRight += PlayRight;
    }

    private void SetJumpAnimations()
    {
        var jump = GetComponent<Jump>();
        jump.OnJumpLeft += PlayJumpLeft;
        jump.OnJumpRight += PlayJumpRight;
    }

    private void SetExplodeAnimations()
    {
        var explode = GetComponent<Explode>();
        explode.OnExplode += PlayExplode;
    }

    private void SetRespawnAnimations()
    {
        var respawn = GetComponent<Respawn>();
        respawn.OnRespawn += PlayRespawn;
    }

    public void Play(AnimationClip clip)
    {
        if (currentClip != clip)
        {
            anim.Play(clip.name);
            currentClip = clip;
        }
    }

    IEnumerator Playing(AnimationClip clip)
    {
        Play(clip);
        yield return new WaitForSeconds(clip.length);
        if(clip == JUMP_LEFT || clip == JUMP_RIGHT)
        {
            Play(IDLE);
        }
        currentCoroutine = null;
    }

    public void PlayLeft() { if(currentCoroutine == null) Play(MOVE_LEFT); }
    public void PlayRight() { if (currentCoroutine == null) Play(MOVE_RIGHT); }

    public void PlayCoroutine(AnimationClip clip)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(Playing(clip));
    }

    public void PlayJumpLeft() { PlayCoroutine(JUMP_LEFT); }
    public void PlayJumpRight() { PlayCoroutine(JUMP_RIGHT); }
    public void PlayExplode() { PlayCoroutine(EXPLODE); }
    public void PlayRespawn() { PlayCoroutine(RESPAWN); }
}
