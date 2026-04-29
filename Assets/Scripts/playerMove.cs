using UnityEngine;
using UnityEngine.InputSystem;

public class TempPlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.2f;
    public float gravity = -9.81f;

    [Header("Camera Assignment")]
    public Transform playerCamera;

    private float rotationX = 0f;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            rotationX -= mouseDelta.y * mouseSensitivity;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            transform.Rotate(Vector3.up * (mouseDelta.x * mouseSensitivity));
        }

        Vector3 move = Vector3.zero;
        if (Keyboard.current != null)
        {
            float moveForward = 0f;
            float moveSide = 0f;

            if (Keyboard.current.wKey.isPressed) moveForward += 1f;
            if (Keyboard.current.sKey.isPressed) moveForward -= 1f;
            if (Keyboard.current.aKey.isPressed) moveSide -= 1f;
            if (Keyboard.current.dKey.isPressed) moveSide += 1f;

            if (Keyboard.current.upArrowKey.isPressed) moveForward += 1f;
            if (Keyboard.current.downArrowKey.isPressed) moveForward -= 1f;
            if (Keyboard.current.leftArrowKey.isPressed) moveSide -= 1f;
            if (Keyboard.current.rightArrowKey.isPressed) moveSide += 1f;

            move = transform.right * moveSide + transform.forward * moveForward;
        }

        if (controller != null)
        {
            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; 
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move((move * moveSpeed + velocity) * Time.deltaTime);
        }
    }
}