using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float MouseSensitivity = 10f;
    [SerializeField] private float jumpForce;
    private float gravityValue = -9.81f;

    Animator anim;
    CharacterController controller;
    [SerializeField]private bool m_isGrounded;
    public bool canMove = false;
    public bool canJump = false;
    private Vector3 playerVelocity;
    private List<Collider> m_collisions = new List<Collider>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            anim.SetBool("Grounded", m_isGrounded);
            Move();
        }
    }

    private void Update()
    {
        m_isGrounded = controller.isGrounded;
        if (m_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (canJump)
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        if (canMove)
        {
            Rotatation();
        }
    }

    private void Jump()
    {
        if (m_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.PlayMusic("Jump");
                anim.SetTrigger("Jump");
                playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
                m_isGrounded = false;
            }
        }
    }

    private void Rotatation()
    {
        float x = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        Vector3 rot = new Vector3(0, x, 0);
        transform.Rotate(rot);
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * moveSpeed);
        anim.SetFloat("MoveSpeed", move.magnitude);
    }
    public void AddJumpForce(float amount)
    {
        jumpForce += amount;
    }

    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
