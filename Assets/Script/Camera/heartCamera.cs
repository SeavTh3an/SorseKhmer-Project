using UnityEngine;

public class HeartFollowCamera2D : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 initialScale;

    void Start()
    {
        mainCamera = Camera.main;

        // Save initial offset between heart and camera
        offset = transform.position - mainCamera.transform.position;

        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Follow camera automatically
        transform.position = mainCamera.transform.position + offset;

        // Keep original scale
        transform.localScale = initialScale;
    }
}
