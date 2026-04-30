using UnityEngine;
using UnityEngine.InputSystem;

public class LabFlowManager : MonoBehaviour
{
    public static LabFlowManager Instance;
    private bool syringePicked = false;
    private bool canPressH = false; 

    void Awake() { Instance = this; }

    void Start()
    {
        Invoke("ShowHButtonHint", 30f);
    }

    void Update()
    {
        if (canPressH && !syringePicked && Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame)
        {
            UIManager.Instance.ShowMessage("Hint: Look for the syringes on the desk...");

            Invoke("ClearBottomHint", 4f);
        }
    }

    public void PlayerReachedTubes() { }

    void ShowHButtonHint()
    {
        if (!syringePicked)
        {
            UIManager.Instance.ShowTopHint("Press 'H' for Hint");
            canPressH = true; 
        }
    }

    void ClearBottomHint()
    {
        UIManager.Instance.ClearMessage();
    }

    public void PlayerPickedSyringe()
    {
        syringePicked = true;
        canPressH = false;
        UIManager.Instance.ClearTopHint(); 
    }
}