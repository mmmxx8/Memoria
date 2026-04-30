using UnityEngine;

public class CabinetInteract : MonoBehaviour
{
    [Header("Door Meshes")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Hinge Points")]
    public Transform leftHinge;
    public Transform rightHinge;

    [Header("Animation Settings")]
    public float leftOpenAngle = -90f; // Change to 90f if it opens inwards
    public float rightOpenAngle = 90f; // Change to -90f if it opens inwards
    public float openSpeed = 4f;

    private bool isOpen = false;
    private float currentWeight = 0f;

    // Store initial positions and rotations
    private Vector3 leftInitialPos, rightInitialPos;
    private Quaternion leftInitialRot, rightInitialRot;

    void Start()
    {
        // Save the initial state before the game starts
        if (leftDoor != null)
        {
            leftInitialPos = leftDoor.position;
            leftInitialRot = leftDoor.rotation;
        }
        if (rightDoor != null)
        {
            rightInitialPos = rightDoor.position;
            rightInitialRot = rightDoor.rotation;
        }
    }

    void Update()
    {
        // Calculate the movement weight from 0 (closed) to 1 (open)
        float targetWeight = isOpen ? 1f : 0f;
        currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * openSpeed);

        // Rotate left door around the virtual hinge point
        if (leftDoor != null && leftHinge != null)
        {
            float currentAngle = leftOpenAngle * currentWeight;
            Quaternion rot = Quaternion.Euler(0, currentAngle, 0);
            leftDoor.position = leftHinge.position + rot * (leftInitialPos - leftHinge.position);
            leftDoor.rotation = rot * leftInitialRot;
        }

        // Rotate right door around the virtual hinge point
        if (rightDoor != null && rightHinge != null)
        {
            float currentAngle = rightOpenAngle * currentWeight;
            Quaternion rot = Quaternion.Euler(0, currentAngle, 0);
            rightDoor.position = rightHinge.position + rot * (rightInitialPos - rightHinge.position);
            rightDoor.rotation = rot * rightInitialRot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
            UIManager.Instance.ShowMessage("These chemical flasks are highly reactive. Handle with care.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            UIManager.Instance.ClearMessage();
        }
    }
}