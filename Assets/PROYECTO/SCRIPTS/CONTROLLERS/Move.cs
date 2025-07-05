using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float movX;
    private float movZ;

    private float crouchSpeed = 2;
    private float walkspeed = 4f;
    private float runSpeed = 5;

    private CharacterController charCtrl;

    private float gravedad = -30f;
    private float fuerxaSalto = 1;
    private Vector3 moveVert;
    private bool isGrounded;
    private Transform groundCheck;

    private float radius;
    [SerializeField]
    private LayerMask whatIsGround;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    void Update()
    {
        CharacterMove();

        CharacterJump();
    }

    public void CharacterMove()
    {
        charCtrl.Move(Speed() * Time.deltaTime * Movement());
    }
    public void CharacterJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, whatIsGround);

        if (isGrounded && moveVert.y < 0)
        {
            moveVert.y = 0;
        }

        if (JumpInputPressed() && isGrounded)
        {
            moveVert.y = Mathf.Sqrt(fuerxaSalto * -2 * gravedad);
        }

        moveVert.y += gravedad * Time.deltaTime;

        charCtrl.Move(moveVert * Time.deltaTime);
    }
    public Vector3 Movement()
    {
        Vector3 movimiento = transform.right * HorizontalAxis() + transform.forward * VerticalAxis();
        return movimiento;
    }
    public float Speed()
    {
        return RunInputPressed() ? runSpeed : CrouchInputPressed() ? crouchSpeed : walkspeed;
    }
    private float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
    private float VerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
    public bool RunInputPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public bool CrouchInputPressed()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }
    public bool JumpInputPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
