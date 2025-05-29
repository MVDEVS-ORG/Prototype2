using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    //public float jumpHeight = 1.5f;

    [Header("Mouse Look Settings")]
    public Transform cameraPos;
    public float mouseSensitivity = 100f;
    public float maxLookAngle = 80;

    [SerializeField] private CharacterController _controller;
    private Vector3 _velocity;
    private float _verticalLookRotation = 0f;
    private bool _isGrounded;

    [SerializeField] private bool LockCursor;

    private void Start()
    {
        if (LockCursor)
        {
            _controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensitivity * Time.deltaTime;

        _verticalLookRotation -= mouseY;
        _verticalLookRotation = Mathf.Clamp( _verticalLookRotation, -maxLookAngle, maxLookAngle );
        cameraPos.localRotation = Quaternion.Euler(_verticalLookRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        _isGrounded = _controller.isGrounded;
        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        _controller.Move(move * moveSpeed * Time.deltaTime);

        /*if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
