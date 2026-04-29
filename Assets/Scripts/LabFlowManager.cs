using UnityEngine;

public class LabFlowManager : MonoBehaviour
{
    public static LabFlowManager Instance;
    public GameObject hintArrow;
    private bool syringePicked = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (hintArrow != null)
        {
            hintArrow.SetActive(false);
        }

        Invoke("ShowSyringeHint", 30f);
    }

    public void PlayerReachedTubes()
    {
    }

    void ShowSyringeHint()
    {
        if (!syringePicked && hintArrow != null)
        {
            hintArrow.SetActive(true);
        }
    }

    public void PlayerPickedSyringe()
    {
        syringePicked = true;
        if (hintArrow != null)
        {
            hintArrow.SetActive(false);
        }
    }
}