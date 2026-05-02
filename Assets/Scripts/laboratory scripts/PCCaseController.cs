using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement; 

public class PCCaseController : MonoBehaviour
{
    [Header("End Game Sequence")]
    public Camera mainPlayerCamera;
    public Camera winCamera;
    public VideoPlayer pcScreenVideo;
    public GameObject uiToHide;

    [Header("Animation Settings")]
    public float transitionSpeed = 1.5f;
    public float videoDuration = 3f;

    [Header("Menu Settings")]
    public string menuSceneName = "end game menu";

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        if (winCamera != null)
        {
            targetPosition = winCamera.transform.position;
            targetRotation = winCamera.transform.rotation;
        }
    }

    public void InsertFlashDrive()
    {
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        InteractionScript interaction = FindAnyObjectByType<InteractionScript>();
        if (interaction != null) interaction.enabled = false;

        UIManager.Instance.ShowMessage("Flash Drive inserted successfully!");
        yield return new WaitForSeconds(2f);

        UIManager.Instance.ClearMessage();
        if (uiToHide != null) uiToHide.SetActive(false);

        if (winCamera != null && mainPlayerCamera != null)
        {
            winCamera.transform.position = mainPlayerCamera.transform.position;
            winCamera.transform.rotation = mainPlayerCamera.transform.rotation;

            mainPlayerCamera.gameObject.SetActive(false);
            winCamera.gameObject.SetActive(true);

            float t = 0;
            Vector3 startPos = winCamera.transform.position;
            Quaternion startRot = winCamera.transform.rotation;

            while (t < 1f)
            {
                t += Time.deltaTime * transitionSpeed;
                float smoothT = Mathf.SmoothStep(0, 1, t);
                winCamera.transform.position = Vector3.Lerp(startPos, targetPosition, smoothT);
                winCamera.transform.rotation = Quaternion.Slerp(startRot, targetRotation, smoothT);
                yield return null;
            }

            winCamera.transform.position = targetPosition;
            winCamera.transform.rotation = targetRotation;
        }

        if (pcScreenVideo != null)
        {
            pcScreenVideo.gameObject.SetActive(true);
            pcScreenVideo.Play();
        }

        yield return new WaitForSeconds(videoDuration);

        if (pcScreenVideo != null)
        {
            pcScreenVideo.gameObject.SetActive(false);
        }

        if (mainPlayerCamera != null && winCamera != null)
        {
            Vector3 targetEuler = winCamera.transform.eulerAngles;
            float pitch = targetEuler.x;
            if (pitch > 180f) pitch -= 360f;
            Quaternion finalPlayerHeadRotation = Quaternion.Euler(pitch, 0, 0);
            Quaternion finalOverallRotation = Quaternion.Euler(pitch, targetEuler.y, 0);

            float reverseT = 0;
            Vector3 reverseStartPos = winCamera.transform.position;
            Quaternion reverseStartRot = winCamera.transform.rotation;
            Vector3 playerPos = mainPlayerCamera.transform.position;

            while (reverseT < 1f)
            {
                reverseT += Time.deltaTime * transitionSpeed;
                float smoothT = Mathf.SmoothStep(0, 1, reverseT);
                winCamera.transform.position = Vector3.Lerp(reverseStartPos, playerPos, smoothT);
                winCamera.transform.rotation = Quaternion.Slerp(reverseStartRot, finalOverallRotation, smoothT);
                yield return null;
            }

            Transform playerBody = mainPlayerCamera.transform.parent;
            if (playerBody != null)
            {
                playerBody.rotation = Quaternion.Euler(0, targetEuler.y, 0);
                mainPlayerCamera.transform.localRotation = finalPlayerHeadRotation;

                TempPlayerMove playerMove = playerBody.GetComponent<TempPlayerMove>();
                if (playerMove != null) playerMove.SyncCamera();
            }

            winCamera.gameObject.SetActive(false);
            mainPlayerCamera.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(menuSceneName);
    }
}