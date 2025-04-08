using UnityEngine;

public class MovementController : MonoBehaviour
{

    private float crouchSpeed = 2;
    private float walkspeed = 3;
    private float runSpeed = 5;

    [SerializeField] private float jumpForce = 5.3f;

    private Rigidbody rb;


    private void Start()
    {
        rb = rb == null ? GetComponent<Rigidbody>() : rb;
    }

    private void FixedUpdate()
    {
        Move();
        //Jump();
    }
    private void Move() 
    {
        rb.velocity = transform.rotation * new Vector3(HorizontalAxis(), rb.velocity.y, VerticalAxis()) * Speed(); 
    }

    public float Speed()
    {
        return RunInputPressed() ? runSpeed : CrouchInputPressed() ? crouchSpeed : walkspeed;
    }

    private void Jump()
    {
        if (JumpInputPressed())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal"); 
    }

    private float VerticalAxis()
    {
        return Input.GetAxis("Vertical"); 
    }

    public bool JumpInputPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool RunInputPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool CrouchInputPressed()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }
}
