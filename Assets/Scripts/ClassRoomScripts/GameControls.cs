using UnityEngine;

public class GameControls : MonoBehaviour
{
   public void QuitGame()
    {
        // This will close the application in a build
        Application.Quit();

        // This line is just for you while testing in Unity
        // It allows the 'Quit' button to stop the play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Debug.Log("Game is exiting...");
    }
}
