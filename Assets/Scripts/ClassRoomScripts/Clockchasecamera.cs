using UnityEngine;

/// <summary>
/// Attach to the Clock object.
/// The clock chases the player from BEHIND — always faster, can never be escaped.
/// </summary>
public class ClockChaseCamera : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;

    [Header("Chase Settings")]
    [Tooltip("How much faster the clock is than the player — set above 20 to always catch up")]
    public float chaseSpeed = 25f;

    [Tooltip("How close behind the player the clock stops")]
    public float stoppingDistance = 2f;

    [Tooltip("Height offset so it floats at back/shoulder level")]
    public float heightOffset = 0.5f;

    [Tooltip("How fast the clock rotates to always face the player")]
    public float rotateSpeed = 8f;

    void Awake()
    {
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (playerCamera == null) return;

        // Target = right BEHIND the player
        Vector3 target = playerCamera.position
                       - playerCamera.forward * stoppingDistance
                       + Vector3.up * heightOffset;

        // Move toward target at chaseSpeed — always faster than the player
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            chaseSpeed * Time.deltaTime
        );

        // Always face the player
        Vector3 dirToPlayer = playerCamera.position - transform.position;
        if (dirToPlayer != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dirToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }
}