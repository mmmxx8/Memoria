using UnityEngine;

public class TubesTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowMessage("The smell is very strong... This is a chemical analysis.");
            LabFlowManager.Instance.PlayerReachedTubes();
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