using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI subtitleText;

    void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        subtitleText.text = message;
    }

    public void ClearMessage()
    {
        subtitleText.text = "";
    }
}