using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Jump : MonoBehaviour
{
    public event Action OnJumpLeft;
    public event Action OnJumpRight;
    public LayerMask platformLayer;
    public LayerMask playerLayer;
    public Transform groundCheck;
    public float JUMP_SPEED;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (PlayerPressesUp() && IsGrounded())
        {
            SetVelocity();
        }
    }

    private void SetVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, JUMP_SPEED);
        if (rb.velocity.x < 0)
        {
            OnJumpLeft?.Invoke();
            return;
        }
        OnJumpRight?.Invoke();
    }

    private bool PlayerPressesUp() { return Input.GetAxis("Vertical") > 0; }

    private bool IsGrounded()
    {
        Vector2 checkPosition = groundCheck.position;
        Vector3 checkSize = new Vector2(boxCollider.bounds.size.x - 0.2f, 0.1f);
        RaycastHit2D platformHit = Physics2D.BoxCast(checkPosition, checkSize, 0, Vector2.down, 0.1f, platformLayer);
        RaycastHit2D[] playerHits = Physics2D.BoxCastAll(checkPosition, checkSize, 0, Vector2.down, 0.1f, playerLayer);
        foreach (var playerHit in playerHits)
        {
            if (playerHit)
            {
                if (!playerHit.transform.gameObject.Equals(transform.gameObject))
                {
                    return true;
                }
            }
        }
        return platformHit.collider != null;
    }
}
