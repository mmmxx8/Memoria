using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public float speed = 1f;
    public TextMeshProUGUI timerText;

    public ClassRoomClue clue;

    public bool isRunning = true;

    private Color originalColor;

    void Start()
    {
        // store original text color
        originalColor = timerText.color;
    }

    void Update()
    {
        if (!isRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime * speed;
            timerText.text = Mathf.Ceil(timeRemaining).ToString();


            // 🎨 COLOR LOGIC
            if (timeRemaining <= 10f)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = originalColor;
            }
        }
        else
        {
            timeRemaining = 0;
            timerText.text = "0";
            timerText.color = Color.red;

            isRunning = false;

            // ⏰ show wrong when time ends
            clue.puzzlePanel.SetActive(false);
            clue.wrongPanel.SetActive(true);

            clue.board.LockAsLost();
        }
    }
}