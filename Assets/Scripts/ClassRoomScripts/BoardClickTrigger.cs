using UnityEngine;

public class BoardClickTrigger : MonoBehaviour
{
    public GameObject popupCanvas; // Drag your Canvas Panel here

    void Start()
    {
        // Hide the popup at the start of the game
        if (popupCanvas != null)
            popupCanvas.SetActive(false);
    }

    // This function runs when the mouse clicks the object's collider
    void OnMouseDown()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(true);
            
            // Optional: Unlock the mouse so the player can type
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}