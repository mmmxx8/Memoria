using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// First-Person Camera Controller — New Input System version
/// ► Attach directly to your Main Camera
/// ► Requires: Window > Package Manager > Input System (already installed)
///
/// Controls:
///   W / A / S / D   — Walk
///   Mouse           — Look around
///   Left Shift      — Run
///   P               — Toggle stop / resume
///   Escape          — Free cursor
/// </summary>
public class CameraFPSController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed  = 8f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 0.15f;  // lower value = less twitchy with new Input System
    public float verticalClamp    = 80f;

    // ── private ──────────────────────────────────────────────
    private float _xRotation = 0f;
    private bool  _paused    = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        var mouse    = Mouse.current;

        if (keyboard == null || mouse == null) return;

        // ── P = pause / resume ────────────────────────────────
        if (keyboard.pKey.wasPressedThisFrame)
        {
            _paused = !_paused;
            Cursor.lockState = _paused ? CursorLockMode.None  : CursorLockMode.Locked;
            Cursor.visible   = _paused;
        }

        // ── Escape = free cursor ──────────────────────────────
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible   = true;
        }

        // ── Left click = re-lock cursor ───────────────────────
        if (mouse.leftButton.wasPressedThisFrame && Cursor.lockState != CursorLockMode.Locked && !_paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible   = false;
        }

        if (_paused) return;

        HandleLook(mouse);
        HandleMove(keyboard);
    }

    void HandleLook(Mouse mouse)
    {
        Vector2 mouseDelta = mouse.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // Horizontal rotation (left / right)
        transform.Rotate(Vector3.up * mouseX, Space.World);

        // Vertical tilt (up / down) — clamped
        _xRotation -= mouseY;
        _xRotation  = Mathf.Clamp(_xRotation, -verticalClamp, verticalClamp);

        Vector3 euler = transform.eulerAngles;
        transform.eulerAngles = new Vector3(_xRotation, euler.y, 0f);
    }

    void HandleMove(Keyboard keyboard)
    {
        bool running = keyboard.leftShiftKey.isPressed;
        float speed  = running ? runSpeed : walkSpeed;

        // Read WASD + Arrow keys
        float h = 0f, v = 0f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) h += 1f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)  h -= 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)    v += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)  v -= 1f;

        // Move relative to camera facing direction (ignore vertical tilt for ground movement)
        Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 flatRight   = new Vector3(transform.right.x,   0f, transform.right.z).normalized;

        Vector3 move = (flatForward * v + flatRight * h).normalized;
        transform.position += move * speed * Time.deltaTime;
    }
}