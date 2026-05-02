using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuController : MonoBehaviour
{
    public string mainGameScene = "First Scene"; 
    public string startMenuScene = "start game menu";

    public void RestartGame()
    {
        SceneManager.LoadScene(mainGameScene);
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
}