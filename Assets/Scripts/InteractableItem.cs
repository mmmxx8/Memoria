using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [TextArea]
    public string hoverMessage = "Place message here...";
    
    [Header("Object Identity")]
    public bool isTubes = false; 
    public bool isMicroscope = false; // Add this line

    public void OnInteract()
    {
        if (isTubes)
        {
            LabFlowManager.Instance.PlayerReachedTubes();
        }
        
        // NEW: Trigger microscope logic
        if (isMicroscope)
        {
            Debug.Log("Microscope logic triggered!");
            // LabFlowManager.Instance.PlayerReachedMicroscope(); // If you have this method
        }
    }
}