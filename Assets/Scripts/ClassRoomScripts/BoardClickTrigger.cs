using UnityEngine;

public class BoardClickTrigger : MonoBehaviour
{
    public GameObject popupCanvas; // Drag your Canvas Panel here
    private bool isLocked = false;

    void Start()
    {
        // Hide the popup at the start of the game
        if (popupCanvas != null)
            popupCanvas.SetActive(false);
    }

    // This function runs when the mouse clicks the object's collider
    void OnMouseDown()
    {
        // 🚫 block interaction permanently
        if (isLocked) return;

        if (popupCanvas != null)
        {
            popupCanvas.SetActive(true);
            
            // Optional: Unlock the mouse so the player can type
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

     // 🟢 call this when player WINS
    public void LockAsWon()
    {
        isLocked = true;
    }

    // 🔴 call this when player LOSES
    public void LockAsLost()
    {
        isLocked = true;
    }
}