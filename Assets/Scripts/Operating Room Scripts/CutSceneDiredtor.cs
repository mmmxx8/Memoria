using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneDirector : MonoBehaviour
{
    [Header("Doctor Speech Bubble")]
    public TextMeshProUGUI doctorSpeechText;
    public GameObject speechBubbleGroup; 

    [Header("Player Screen")]
    public Image blackoutImage; 

    [Header("Settings")]
    public float timeBetweenLines = 4.5f; 

    private string[] dialogueLines = {
        "I'm sorry it had to come to this... but you're infected too.",
        "Almost the entire world has fallen. You are our last hope.",
        "I've just administered the experimental vaccine... You are the only viable subject.",
        "There are two outcomes: your body accepts it and you fall into a coma, or...",
        "If you survive, you will wake up with no memory of this. No memory of who you are.",
        "Listen closely... To recreate the serum, you must first extract the blue compound from..."
    };

    void Start()
    {
        if (blackoutImage != null)
        {
            Color c = blackoutImage.color;
            c.a = 0f;
            blackoutImage.color = c;
        }

        StartCoroutine(PlayCutsceneSequence());
    }

    IEnumerator PlayCutsceneSequence()
    {
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            doctorSpeechText.text = dialogueLines[i];

            if (i == dialogueLines.Length - 1)
            {
                StartCoroutine(FadeToBlack(4f)); 
            }

            yield return new WaitForSeconds(timeBetweenLines);
        }

        doctorSpeechText.text = "";
        if (speechBubbleGroup != null) speechBubbleGroup.SetActive(false);
    }

    IEnumerator FadeToBlack(float duration)
    {
        float elapsedTime = 0f;
        Color c = blackoutImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / duration); 
            blackoutImage.color = c;
            yield return null;
        }

       // here next scene
    }
}