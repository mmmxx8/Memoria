using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform targetToFollow; 
    public Vector3 offset = new Vector3(0, 2f, 0);
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (targetToFollow != null)
        {
            transform.position = targetToFollow.position + offset;
        }

        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}