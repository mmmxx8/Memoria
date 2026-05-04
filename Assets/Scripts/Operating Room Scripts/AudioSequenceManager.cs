using UnityEngine;
using System.Collections;

public class AudioSequenceManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public float audioDuration = 45f;
    public string nextSceneName = "First Scene";

    void Start()
    {
        StartCoroutine(WaitByTimeAndTransition());
    }

    IEnumerator WaitByTimeAndTransition()
    {
        yield return new WaitForSeconds(audioDuration);

        yield return new WaitForSeconds(0.5f);

       
        InteractionScript.hasCompletedOperatingRoom = true;

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.StartTransition(nextSceneName);
        }
        else
        {
            Debug.LogError("SceneTransitionManager not found! Make sure you started from the Lab scene.");
        }
    }
}