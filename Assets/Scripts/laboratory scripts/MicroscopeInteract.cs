using UnityEngine;

public class MicroscopeInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowMessage("Looking closely... the blood cells look abnormal.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ClearMessage();
        }
    }
}