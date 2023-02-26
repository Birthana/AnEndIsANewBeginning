using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
public class Jump : MonoBehaviour
{
    public event Action OnJumpLeft;
    public event Action OnJumpRight;
    public LayerMask platformLayer;
    public LayerMask playerLayer;

    public Transform groundCheck;
    public Vector2 checkBounds;
    private BoxCollider2D boxCollider;

    public float JUMP_SPEED;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        isJumping = false;
    }

    private void Update()
    {
        if (PlayerPressesUp() && CanJump() && !isJumping)
        {
            StartCoroutine(Jumping());
        }
    }

    IEnumerator Jumping()
    {
        isJumping = true;
        SetVelocity();
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }

    private void SetVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, JUMP_SPEED);
        if (playerMovement.IsFacingLeft())
        {
            OnJumpLeft?.Invoke();
            return;
        }
        OnJumpRight?.Invoke();
    }

    public bool IsJumping() { return rb.velocity.y > 0 && !CanJump(); }
    private bool PlayerPressesUp() { return Input.GetAxis("Vertical") > 0; }
    private Vector2 GetGroundCheck() { return groundCheck.position; }
    private Vector2 GetCheckSize() { return new Vector2(boxCollider.bounds.size.x - checkBounds.x, checkBounds.y); }

    public bool CanJump() { return IsGround() || IsOnPlayer(); }

    private bool IsGround()
    {
        RaycastHit2D[] platformHit = GetHits(platformLayer);
        foreach (var platform in platformHit)
        {
            if (platform.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsOnPlayer()
    {
        RaycastHit2D[] playerHits = GetHits(playerLayer);
        foreach (var playerHit in playerHits)
        {
            if (playerHit)
            {
                if (!HitIsSelf(playerHit) && !HitIsJumping(playerHit))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private RaycastHit2D[] GetHits(LayerMask layer)
    {
        Vector2 checkPosition = GetGroundCheck();
        Vector2 checkSize = GetCheckSize();
        return Physics2D.BoxCastAll(checkPosition, checkSize, 0, Vector2.down, 0.1f, layer);
    }

    private bool HitIsSelf(RaycastHit2D playerHit) { return playerHit.transform.gameObject.Equals(transform.gameObject); }
    private bool HitIsJumping(RaycastHit2D playerHit) { return playerHit.transform.GetComponent<Jump>().IsJumping(); }
}
