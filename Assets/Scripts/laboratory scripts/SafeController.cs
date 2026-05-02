using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class SafeController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject safeUIPanel;
    public TMP_InputField passwordInputField;
    public GameObject gameplayUI;

    [Header("Animation Settings")]
    public Transform knobBone;
    public Transform handleBone;
    public Transform hingeBone;
    public float openSpeed = 2f;

    [Header("Flash Drive & Physics")]
    public Collider flashDriveCollider;
    public Collider safeCollider;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;

    private bool isUIActive = false;
    private bool isOpened = false;
    private string correctPassword = "1234";

    void Update()
    {
        if (isUIActive)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame) CloseUI();
            if (Keyboard.current.enterKey.wasPressedThisFrame) CheckPassword();
        }
    }

    public void OpenPasswordUI()
    {
        if (isOpened) return;
        isUIActive = true;
        safeUIPanel.SetActive(true);
        if (gameplayUI != null) gameplayUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        passwordInputField.ActivateInputField();
        passwordInputField.text = "";
    }

    public void CheckPassword()
    {
        if (passwordInputField.text.Trim() == correctPassword)
        {
            StartCoroutine(OpenSafeSequence());
            CloseUI();
        }
        else
        {
            passwordInputField.text = "";
            passwordInputField.ActivateInputField();
        }
    }

    public void CloseUI()
    {
        isUIActive = false;
        safeUIPanel.SetActive(false);
        if (gameplayUI != null) gameplayUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator OpenSafeSequence()
    {
        isOpened = true;

        if (audioSource && openSound)
            audioSource.PlayOneShot(openSound);

        yield return RotateBone(knobBone, new Vector3(0, 0, 360), 1f);

        yield return RotateBone(handleBone, new Vector3(0, 0, 90), 0.5f);

        yield return RotateBone(hingeBone, new Vector3(0, -90, 0), 1.2f);


        if (flashDriveCollider != null)
        {
            flashDriveCollider.enabled = true;
        }

        if (safeCollider != null)
        {
            safeCollider.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    IEnumerator RotateBone(Transform bone, Vector3 rotationAmount, float duration)
    {
        if (bone == null) yield break;
        Quaternion startRot = bone.localRotation;
        Quaternion endRot = bone.localRotation * Quaternion.Euler(rotationAmount);
        float elapsed = 0;
        while (elapsed < duration)
        {
            bone.localRotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime * openSpeed;
            yield return null;
        }
        bone.localRotation = endRot;
    }
}