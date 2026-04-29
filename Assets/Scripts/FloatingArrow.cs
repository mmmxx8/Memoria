using UnityEngine;

public class FloatingArrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    public float speed = 0.002f;
    public float height = 0.0015f;
    private Vector3 posContext;

    void Start()
    {
        posContext = transform.position;
    }

    void Update()
    {
        double newY = 0.5*(Mathf.Sin(Time.time * speed) * height);
        transform.position = posContext + new Vector3(0, (float)newY, 0);
    }
}