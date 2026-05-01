using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneDirector : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI doctorSpeechText;
    public GameObject speechBubbleGroup;
    public Image blackoutImage;
    public DoctorAction doctorScript;

    [Header("Settings")]
    public float timeBetweenLines = 5f;

    private string[] dialogueLines = {
        "I'm sorry it had to come to this... but you're infected too.", // Line 0 
        "Almost the entire world has fallen. You are our last hope.", // Line 1
        "I've just administered the experimental vaccine... You are the only viable subject.", // Line 2 
        "There are two outcomes: your body accepts it and you fall into a coma, or...", // Line 3 
        "If you survive, you will wake up with no memory of this. No memory of who you are.", // Line 4 
        "Listen closely... To recreate the serum, you must first extract the blue compound from..." // Line 5 
    };

    void Start()
    {
        if (blackoutImage != null) { Color c = blackoutImage.color; c.a = 0f; blackoutImage.color = c; }
        StartCoroutine(PlayCutsceneSequence());
    }

    IEnumerator PlayCutsceneSequence()
    {
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            doctorSpeechText.text = dialogueLines[i];

            if (i == 1)
            {
                doctorScript.StartWalkingToTable();
            }

            if (i == 3)
            {
                doctorScript.StartWalkingToPlayer(); 
            }

            if (i == dialogueLines.Length - 1)
            {
                StartCoroutine(FadeToBlack(5f));
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
    }
}