using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action OnLeft;
    public event Action OnRight;
    [SerializeField] private float SPEED;
    [Range(0, 1)] [SerializeField] private float ACCELERATION;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (PlayerPressesRight())
        {
            MoveRight();
        }
        
        if (PlayerPressesLeft())
        {
            MoveLeft();
        }

        if (!PlayerPressesLeft() && !PlayerPressesRight() && IsMoving())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool IsMoving()
    {
        return rb.velocity.x != 0;
    }

    private bool PlayerPressesRight()
    {
        return Input.GetAxis("Horizontal") > ACCELERATION;
    }

    private bool PlayerPressesLeft()
    {
        return Input.GetAxis("Horizontal") < -ACCELERATION;
    }

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
