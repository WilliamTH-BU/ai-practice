using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;
    public float RotateSpeed = 120f;
    public float JumpForce = 12f;
    private bool isGrounded = true;
    private float moveForward = 0;
    private float rotateSpeed = 0;

    AudioClip monkeySound;
    AudioSource audioSource;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    private void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    public void Move()
    {
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        Vector2 input = moveAction.ReadValue<Vector2>();
        moveForward = input.y;
        if (moveForward > 0)
        {
           transform.Translate(Speed * Time.deltaTime * Vector3.down);
        }
        if (moveForward < 0)
        {
            transform.Translate(Speed * Time.deltaTime * Vector3.up);
        }
    }

    public void Rotate()
    {
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        Vector2 input = moveAction.ReadValue<Vector2>();
        rotateSpeed = input.x;
        transform.Rotate(Vector3.forward, rotateSpeed * RotateSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        InputAction jumpAction = InputSystem.actions.FindAction("Jump");

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
