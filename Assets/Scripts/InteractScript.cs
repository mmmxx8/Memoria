using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class InteractionScript : MonoBehaviour
{
    public static bool hasCompletedOperatingRoom = false;

    public float normalInteractRange = 1.0f;
    public float flashDriveInteractRange = 3.0f;
    public LayerMask interactableLayer;
    public Image crosshair;
    public Color normalColor = Color.blue;
    public Color hoverColor = Color.green;

    public static bool hasFlashDrive = false;
    private GameObject lastHoveredObject;
    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if (cam == null) cam = Camera.main;
        if (crosshair != null) crosshair.color = normalColor;
        hasFlashDrive = false;

        if (hasCompletedOperatingRoom)
        {
            InteractableItem[] allItems = FindObjectsOfType<InteractableItem>();
            foreach (InteractableItem item in allItems)
            {
                if (item.isSyringe && !item.isLibrarySyringe)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (cam == null) return;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        float maxRange = Mathf.Max(normalInteractRange, flashDriveInteractRange);

        if (Physics.Raycast(ray, out hit, maxRange, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            InteractableItem item = hitObject.GetComponent<InteractableItem>();

            if (item != null)
            {
                if (hit.distance > normalInteractRange && !item.isFlashDrive)
                {
                    OnHoverExit();
                    return;
                }
                if (hitObject != lastHoveredObject) OnHoverEnter(hitObject);

                if (Keyboard.current.iKey.wasPressedThisFrame && item.isSyringe)
                {
                    if (item.isLibrarySyringe && !hasCompletedOperatingRoom)
                    {
                        if (UIManager.Instance != null)
                            UIManager.Instance.ShowMessage("You need to remember the operation first...");
                        return;
                    }

                    string sceneToLoad = item.targetSceneName;
                    hitObject.SetActive(false);
                    OnHoverExit();

                    TempPlayerMove playerScript = FindObjectOfType<TempPlayerMove>();
                    if (playerScript != null)
                    {
                        playerScript.SavePlayerState();
                    }

                    if (SceneTransitionManager.Instance != null)
                    {
                        SceneTransitionManager.Instance.StartTransition(sceneToLoad);
                    }
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (item.isPCCase && hasFlashDrive)
                    {
                        PCCaseController pc = hitObject.GetComponent<PCCaseController>();
                        if (pc != null) pc.InsertFlashDrive();
                    }
                    else if (!item.isPCCase) item.OnInteract();
                }

                if (Keyboard.current.pKey.wasPressedThisFrame && item.isSafe)
                {
                    SafeController safe = hitObject.GetComponent<SafeController>();
                    if (safe != null) safe.OpenPasswordUI();
                }

                if (Keyboard.current.tKey.wasPressedThisFrame && item.isFlashDrive)
                {
                    TakeFlashDrive(hitObject);
                }
            }
            else OnHoverExit();
        }
        else OnHoverExit();
    }

    void TakeFlashDrive(GameObject flashDrive)
    {
        Debug.Log("Flash drive taken!");
        hasFlashDrive = true;
        flashDrive.SetActive(false);
        OnHoverExit();
    }

    void OnHoverEnter(GameObject obj)
    {
        lastHoveredObject = obj;
        if (crosshair != null) crosshair.color = hoverColor;
        InteractableItem item = obj.GetComponent<InteractableItem>();
        if (item != null)
        {
            if (item.isPCCase)
            {
                if (hasFlashDrive) UIManager.Instance.ShowMessage(item.hoverMessage);
            }
            else UIManager.Instance.ShowMessage(item.hoverMessage);
        }
    }

    void OnHoverExit()
    {
        if (lastHoveredObject != null)
        {
            if (crosshair != null) crosshair.color = normalColor;
            if (UIManager.Instance != null) UIManager.Instance.ClearMessage();
            lastHoveredObject = null;
        }
    }
}