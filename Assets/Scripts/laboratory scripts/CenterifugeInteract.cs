using UnityEngine;

public class CentrifugeInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowMessage("The centrifuge is running. Do not touch it!!!!");
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