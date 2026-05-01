using UnityEngine;
using UnityEngine.SceneManagement; // Required for switching scenes

public class BackToLab : MonoBehaviour
{
    public void LoadLabScene()
    {
        // Define the scene name directly as a string variable inside the function
        string sceneName = "LabScene"; 

        // Load the scene using that string
        SceneManager.LoadScene(sceneName);
    }
}