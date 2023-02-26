using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Explode))]
public class PlayerMovement : MonoBehaviour
{
    public event Action OnLeft;
    public event Action OnRight;
    public enum Direction { Left, Right }

    [SerializeField] private float SPEED;
    [Range(0, 1)] [SerializeField] private float ACCELERATION;
    private Direction direction_;
    private Rigidbody2D rb;
    private bool CanMove;
    private float explosionTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CanMove = true;
        SetExplosion();
        SetDirection(Direction.Right);
    }

    private void SetExplosion()
    {
        var explode = GetComponent<Explode>();
        explode.OnExplode += StopMove;
        explosionTime = explode.GetExplosionTime();
    }

    private void FixedUpdate()
    {
        if (PlayerPressesRight() && CanMove)
        {
            SetDirection(Direction.Right);
            MoveRight();
        }
        
        if (PlayerPressesLeft() && CanMove)
        {
            SetDirection(Direction.Left);
            MoveLeft();
        }

        if (!PlayerPressesLeft() && !PlayerPressesRight() && IsMoving())
        {
            ResetVelocity();
        }
    }

    public bool IsFacingLeft() { return direction_ == Direction.Left; }
    public bool IsFacingRight() { return direction_ == Direction.Right; }
    public void StopMove() { StartCoroutine(StopMoving()); }

    IEnumerator StopMoving()
    {
        CanMove = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(explosionTime);
        rb.gravityScale = 1;
        CanMove = true;
    }

    private void SetDirection(Direction direction) { direction_ = direction; }
    private void ResetVelocity() { rb.velocity = new Vector2(0, rb.velocity.y); }
    private bool IsMoving() { return rb.velocity.x != 0; }
    private bool PlayerPressesRight() { return Input.GetAxis("Horizontal") > ACCELERATION; }
    private bool PlayerPressesLeft() { return Input.GetAxis("Horizontal") < -ACCELERATION; }

    private void MoveRight()
    {
        rb.velocity = new Vector2(Mathf.Abs(Input.GetAxis("Horizontal")) * SPEED, rb.velocity.y);
        OnRight?.Invoke();
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-Mathf.Abs(Input.GetAxis("Horizontal")) * SPEED, rb.velocity.y);
        OnLeft?.Invoke();
    }
}
