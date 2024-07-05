using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float MouseSensitivity = 10f;
    [SerializeField] private float jumpForce;

    Animator anim;
    Rigidbody rb;

    private bool m_isGrounded;
    public bool canMove = false;
    public bool canJump = false;

    private List<Collider> m_collisions = new List<Collider>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                m_isGrounded = false;
            }
        }
    }

    private void Rotatation()
    {
        float x = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        Vector3 rot = new Vector3(0, x, 0).normalized;
        transform.Rotate( rot);
    }

    private void Move()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        if (z != 0)
        {
            if (!AudioManager.instance.CheckAudioIsPlaying("FootStep"))
            {
                AudioManager.instance.PlayMusic("FootStep");
            }
        }
        else
        {
            AudioManager.instance.StopMusic("FootStep");

        }

        Vector3 direction = new Vector3(x, rb.velocity.y, z).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float turnSmoothTime = 0.1f;
            float turnSmoothVelocity = 0;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = moveDir.normalized * moveSpeed;

            anim.SetFloat("MoveSpeed", rb.velocity.magnitude);
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }

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
