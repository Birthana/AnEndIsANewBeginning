using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
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
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
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
        rb.AddForce(SPEED * Vector2.right);
    }

    private void MoveLeft()
    {
        rb.AddForce(SPEED * Vector2.left);
    }
}
