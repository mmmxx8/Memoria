using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach to Main Camera.
/// Shows a red vignette on screen when the clock gets close.
/// 
/// Setup:
/// 1. Create a UI Image (full screen, black/red color, set alpha to 0)
/// 2. Drag it into the "Vignette Image" slot
/// 3. Drag the Clock object into the "Clock" slot
/// </summary>
public class ClockProximityWarning : MonoBehaviour
{
    [Header("References")]
    public Transform clock;
    public Image vignetteImage;   // a full-screen red UI image

    [Header("Settings")]
    [Tooltip("Distance at which the vignette starts appearing")]
    public float dangerDistance = 15f;

    [Tooltip("Distance at which vignette is fully visible")]
    public float maxDangerDistance = 5f;

    void Update()
    {
        if (clock == null || vignetteImage == null) return;

        float dist = Vector3.Distance(transform.position, clock.position);

        // Map distance to alpha: far = 0, close = 1
        float alpha = Mathf.InverseLerp(dangerDistance, maxDangerDistance, dist);
        alpha = Mathf.Clamp01(alpha);

        Color c = vignetteImage.color;
        c.a = alpha * 0.6f;   // max 60% opacity
        vignetteImage.color = c;
    }
}