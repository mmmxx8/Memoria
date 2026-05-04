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

    [Header("Dialogue Content")]
    public string[] dialogueLines = {
        "The vaccine is already bleaching your cortex, Walter. By the time you wake, your name will be the first thing you forget.",
        "I’ve secured your research—every breakthrough, every formula—onto a physical drive. Your life's work is on that flash memory.",
        "I can't leave the encryption key in the lab. If the 'Wrong People' find you, the data has to stay dark.",
        "A simple mind—an infected mind—won't be able to connect the dots.",
        "If you can't figure it out, then you're already too far gone, Walter. Don't let the cure die with me.",
        "Wake up. Find the drive. Be the man I know you are... before the monster takes over."
    };

    [Header("Settings: Time For Each Line")]
    
    public float[] lineDurations = { 9f, 9f, 7f, 5f, 7f, 8f };

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

            float currentLineTime = lineDurations[i];

            if (i == dialogueLines.Length - 1)
            {
                float delayBeforeFade = currentLineTime - 2f;
                StartCoroutine(DelayedFadeToBlack(delayBeforeFade, 2f));
            }

            yield return new WaitForSeconds(currentLineTime);
        }

        doctorSpeechText.text = "";
        if (speechBubbleGroup != null) speechBubbleGroup.SetActive(false);
    }

    IEnumerator DelayedFadeToBlack(float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color c = blackoutImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackoutImage.color = c;
            yield return null;
        }
    }
}