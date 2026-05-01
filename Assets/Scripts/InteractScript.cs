using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractionScript : MonoBehaviour
{
    public float interactRange = 1.0f;
    public LayerMask interactableLayer;
    public Image crosshair;
    public Color normalColor = Color.blue;
    public Color hoverColor = Color.green;

    private GameObject lastHoveredObject;
    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if (cam == null) cam = Camera.main;
        if (crosshair != null) crosshair.color = normalColor;
    }

    void Update()
    {
        if (cam == null) return;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != lastHoveredObject)
            {
                OnHoverEnter(hitObject);
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // Trigger the interaction logic
                InteractableItem item = hitObject.GetComponent<InteractableItem>();
                if (item != null) item.OnInteract();
            }
        }
        else
        {
            OnHoverExit();
        }
    }

    void OnHoverEnter(GameObject obj)
    {
        lastHoveredObject = obj;
        if (crosshair != null) crosshair.color = hoverColor;

        // NEW: Tell the UIManager to show the message
        InteractableItem item = obj.GetComponent<InteractableItem>();
        if (item != null)
        {
            UIManager.Instance.ShowMessage(item.hoverMessage);
        }
    }

    void OnHoverExit()
    {
        if (lastHoveredObject != null)
        {
            if (crosshair != null) crosshair.color = normalColor;
            
            // NEW: Tell the UIManager to clear the message
            UIManager.Instance.ClearMessage();
            
            lastHoveredObject = null;
        }
    }
}