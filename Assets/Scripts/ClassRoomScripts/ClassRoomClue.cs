using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClassRoomClue : MonoBehaviour
{
    public TMP_InputField inputField;
    public int correctAnswer = 20;

    [Header("UI Panels")]
    public GameObject puzzlePanel;
    public GameObject correctPanel;
    public GameObject wrongPanel;

    [Header("Attempts UI")]
    public TextMeshProUGUI attemptsText;
    public BoardClickTrigger board;

    [Header("Timer")]
    public Timer timer;

    int attempts = 0;
    int maxAttempts = 3;
    bool isGameOver = false;

    void Start()
    {
        // Make sure the popup is active before updating UI
        if (puzzlePanel != null)
            puzzlePanel.SetActive(true);

        UpdateAttemptsUI();
    }

    public void SubmitAnswer()
    {
        if (isGameOver) return;

        int playerAnswer;

        if (!int.TryParse(inputField.text, out playerAnswer))
            return;

        if (!timer.isRunning)
            return;

        // ✅ CORRECT
        if (playerAnswer == correctAnswer)
        {
            isGameOver = true;

            puzzlePanel.SetActive(false);
            correctPanel.SetActive(true);
            timer.isRunning = false;

            // 🚫 lock board forever (WIN)
            board.LockAsWon();
            return;
        }

        // ❌ WRONG
        attempts++;
        UpdateAttemptsUI();

        // hide main panel immediately
        puzzlePanel.SetActive(false);

        wrongPanel.SetActive(true);
        Invoke("HideWrong", 1.5f);

        // increase timer speed
        timer.speed *= 1.5f;

        // disable input after max attempts
        if (attempts >= maxAttempts)
        {
            isGameOver = true;

            inputField.interactable = false;
            timer.isRunning = false;

            board.LockAsLost();
             // keep wrong panel visible permanently
            CancelInvoke(nameof(HideWrong));
            return;
        }
    }

    void HideWrong()
    {
        wrongPanel.SetActive(false);
        // 🚫 ONLY RE-OPEN IF PLAYER DID NOT LOSE
        if (attempts < maxAttempts)
        {
           puzzlePanel.SetActive(true);
        }
    }

    void UpdateAttemptsUI()
    {
        if (attemptsText == null || !attemptsText.gameObject.activeInHierarchy)
           return;
           
        int remaining = maxAttempts - attempts;

        if (remaining > 0)
            attemptsText.text = "Attempts Left: " + remaining;
        else
            attemptsText.text = "No Attempts Left";

        // optional color warning
        if (remaining == 1)
            attemptsText.color = Color.red;
    }

    public void GoToLab()
    {
        SceneManager.LoadScene("LabScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}



// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement;

// public class ClassRoomClue : MonoBehaviour
// {
//     public TMP_InputField inputField;
//     public int correctAnswer = 20;

//     [Header("UI Panels")]
//     public GameObject puzzlePanel;   // The main puzzle with input field
//     public GameObject correctPanel;  // Your "Correct Answer!" panel
//     public GameObject wrongPanel;    // Your "Wrong Answer!" panel

//     public void SubmitAnswer()
//     {
//         int playerAnswer;

//         // Try to read the input from the user
//         if (int.TryParse(inputField.text, out playerAnswer))
//         {
//             // First, hide the puzzle panel so one of the results can show
//             if (puzzlePanel != null) puzzlePanel.SetActive(false);

//             if (playerAnswer == correctAnswer)
//             {
//                 Debug.Log("Correct!");
//                 if (correctPanel != null) correctPanel.SetActive(true);
//             }
//             else
//             {
//                 Debug.Log("Wrong!");
//                 if (wrongPanel != null) wrongPanel.SetActive(true);
//             }
//         }
//     }
//     public void GoToLab()
//     {
//         // Replace "LabScene" with the actual name of your lab scene
//         SceneManager.LoadScene("LabScene"); 
//     }

//     // Function for the "Quit" button
//     public void QuitGame()
//     {
//         Debug.Log("Quitting Game...");
//         Application.Quit();
//     }
// }

// public class ClassRoomClue : MonoBehaviour
// {
//     public TMP_InputField inputField;
//     //public int correctAnswer = 8;   // 👈 YOUR STATIC ANSWER
//     public int correctAnswer = 20;   // 👈 YOUR STATIC ANSWER
//     public string nextSceneName;

//     public void SubmitAnswer()
//     {
//         int playerAnswer;

//         if (int.TryParse(inputField.text, out playerAnswer))
//         {
//             if (playerAnswer == correctAnswer)
//             {
//                 Debug.Log("Correct!");
//                 SceneManager.LoadScene("end game menu");
//             }
//             else
//             {
//                 Debug.Log("Wrong!");
//                 LoseGame();
//             }
//         }
//         // Hide the panel after they submit (win or lose)
//         this.gameObject.SetActive(false);
//     }

//     void LoseGame()
//     {
//         Debug.Log("You Lose!");
//         SceneManager.LoadScene("lose menu");
//     }
// }
