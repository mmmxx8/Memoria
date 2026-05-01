// using UnityEngine;

// public class ClassRoomClue : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }



using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClassRoomClue : MonoBehaviour
{
    public TMP_InputField inputField;
    public int correctAnswer = 20;

    [Header("UI Panels")]
    public GameObject puzzlePanel;   // The main puzzle with input field
    public GameObject correctPanel;  // Your "Correct Answer!" panel
    public GameObject wrongPanel;    // Your "Wrong Answer!" panel

    public void SubmitAnswer()
    {
        int playerAnswer;

        // Try to read the input from the user
        if (int.TryParse(inputField.text, out playerAnswer))
        {
            // First, hide the puzzle panel so one of the results can show
            if (puzzlePanel != null) puzzlePanel.SetActive(false);

            if (playerAnswer == correctAnswer)
            {
                Debug.Log("Correct!");
                if (correctPanel != null) correctPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Wrong!");
                if (wrongPanel != null) wrongPanel.SetActive(true);
            }
        }
    }
    public void GoToLab()
    {
        // Replace "LabScene" with the actual name of your lab scene
        SceneManager.LoadScene("LabScene"); 
    }

    // Function for the "Quit" button
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

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
