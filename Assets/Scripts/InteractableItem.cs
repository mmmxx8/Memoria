using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [TextArea]
    public string hoverMessage = "Place message here...";

    [Header("Object Identity")]
    public bool isTubes = false;
    public bool isMicroscope = false;
    public bool isSafe = false;
    public bool isFlashDrive = false;
    public bool isPCCase = false;
    public bool isSyringe = false;

    [Header("Syringe Settings")]
    public string targetSceneName = "Operating RoomScene";
    public bool isLibrarySyringe = false; 

    public void OnInteract()
    {
        if (isTubes)
        {
            LabFlowManager.Instance.PlayerReachedTubes();
        }

        if (isMicroscope)
        {
            Debug.Log("Microscope logic triggered!");
        }
    }
}