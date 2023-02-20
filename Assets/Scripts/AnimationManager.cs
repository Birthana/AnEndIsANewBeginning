using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(Explode))]
[RequireComponent(typeof(Respawn))]
public class AnimationManager : MonoBehaviour
{
    private static readonly int IDLE = Animator.StringToHash("Player_Idle");
    private static readonly int MOVE_RIGHT = Animator.StringToHash("Player_Idle");
    private static readonly int MOVE_LEFT = Animator.StringToHash("Player_Left");
    private static readonly int JUMP_LEFT = Animator.StringToHash("Player_Jump_Left");
    private static readonly int JUMP_RIGHT = Animator.StringToHash("Player_Jump_Right");
    private static readonly int EXPLODE = Animator.StringToHash("Player_Explode");
    private static readonly int RESPAWN = Animator.StringToHash("Player_Respawn");
    private Animator anim;
    private int currentHash;
    private bool IsInterrupted;

    // Start is called before the first frame update
    void Awake()
    {
        IsInterrupted = false;
        anim = GetComponent<Animator>();
        SetMoveAnimations();
        SetJumpAnimations();
        SetExplodeAnimations();
        SetRespawnAnimations();
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

    public void Play(int hash)
    {
        if (currentHash != hash)
        {
            anim.Play(hash);
            currentHash = hash;
        }
    }

    IEnumerator Playing(int hash)
    {
        IsInterrupted = true;
        Play(hash);
        yield return new WaitForSeconds(1.0f);
        IsInterrupted = false;
    }

    public void PlayLeft() { if(!IsInterrupted) Play(MOVE_LEFT); }
    public void PlayRight() { if (!IsInterrupted) Play(MOVE_RIGHT); }
    public void PlayJumpLeft() { StartCoroutine(Playing(JUMP_LEFT)); }
    public void PlayJumpRight() { StartCoroutine(Playing(JUMP_RIGHT)); }
    public void PlayExplode() { StartCoroutine(Playing(EXPLODE)); }
    public void PlayRespawn() { StartCoroutine(Playing(RESPAWN)); }
}
