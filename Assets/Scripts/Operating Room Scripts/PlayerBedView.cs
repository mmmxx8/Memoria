using UnityEngine;

public class PlayerBedView : MonoBehaviour
{
    [Header("Doctor Target")]
    public Transform doctor; 
    public float lookSpeed = 1.5f; 

    void Update()
    {
        if (doctor != null)
        {
            
            Vector3 targetDirection = doctor.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }
}