using UnityEngine;
using UnityEngine.InputSystem; 

public class SyringeInteract : MonoBehaviour
{
    private bool canPickUp = false;

    void Update()
    {
        if (canPickUp && Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame)
        {
            PickUpSyringe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
            UIManager.Instance.ShowMessage("Press 'S' to pick up the syringe.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            UIManager.Instance.ClearMessage();
        }
    }

    void PickUpSyringe()
    {
        UIManager.Instance.ShowMessage("You took the syringe! Now go to the doctor.");
        LabFlowManager.Instance.PlayerPickedSyringe();
        gameObject.SetActive(false);

        Invoke("ClearTextLater", 3f);
    }

    void ClearTextLater()
    {
        UIManager.Instance.ClearMessage();
    }
}