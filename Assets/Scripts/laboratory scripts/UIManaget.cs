using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Bottom Subtitle Settings")]
    public TextMeshProUGUI subtitleText;
    public GameObject subtitlePanel; 

    [Header("Top-Left Hint Settings")]
    public TextMeshProUGUI topLeftText;
    public GameObject topLeftPanel; 

    void Awake() { Instance = this; }

    public void ShowMessage(string message)
    {
        subtitleText.text = message;
        if (subtitlePanel != null) subtitlePanel.SetActive(true);
    }

    public void ClearMessage()
    {
        subtitleText.text = "";
        if (subtitlePanel != null) subtitlePanel.SetActive(false);
    }

    public void ShowTopHint(string message)
    {
        topLeftText.text = message;
        if (topLeftPanel != null) topLeftPanel.SetActive(true);
    }

    public void ClearTopHint()
    {
        topLeftText.text = "";
        if (topLeftPanel != null) topLeftPanel.SetActive(false);
    }
}