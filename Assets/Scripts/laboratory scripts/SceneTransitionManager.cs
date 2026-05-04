using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public RectTransform topBlackBar;
    public RectTransform bottomBlackBar;
    public float transitionSpeed = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTransition(string sceneName)
    {
        StartCoroutine(TransitionRoutine(sceneName));
    }

    IEnumerator TransitionRoutine(string sceneName)
    {
        topBlackBar.sizeDelta = Vector2.zero;
        topBlackBar.anchoredPosition = Vector2.zero;
        bottomBlackBar.sizeDelta = Vector2.zero;
        bottomBlackBar.anchoredPosition = Vector2.zero;

        float elapsed = 0f;
        while (elapsed < transitionSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionSpeed;

            topBlackBar.anchorMin = new Vector2(0, Mathf.Lerp(1, 0.4f, t));
            bottomBlackBar.anchorMax = new Vector2(1, Mathf.Lerp(0, 0.6f, t));

            yield return null;
        }

        Application.backgroundLoadingPriority = ThreadPriority.High;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Application.backgroundLoadingPriority = ThreadPriority.Normal;

        elapsed = 0f;
        while (elapsed < transitionSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionSpeed;

            topBlackBar.anchorMin = new Vector2(0, Mathf.Lerp(0.4f, 1, t));
            bottomBlackBar.anchorMax = new Vector2(1, Mathf.Lerp(0.6f, 0, t));

            yield return null;
        }
    }
}