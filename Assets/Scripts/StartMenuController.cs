using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string mainGameScene = "First Scene";

    public void StartGame()
    {
        SceneManager.LoadScene(mainGameScene);
    }

    public void QuitGameReal()
    {
        Debug.Log("Game is quitting entirely...");
        Application.Quit();
    }
}