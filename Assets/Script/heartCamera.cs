using UnityEngine;

public class HeartFollowCamera2D : MonoBehaviour
{
    public Vector2 screenOffset = new Vector2(50, -50); // Pixels from top-left corner
    private Camera mainCamera;
    private Vector3 initialScale;

    void Start()
    {
        mainCamera = Camera.main;
        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Convert screen position to world position
        Vector3 screenPos = new Vector3(screenOffset.x, Screen.height + screenOffset.y, mainCamera.nearClipPlane + 1f);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

        transform.position = worldPos;

        // Keep original scale (so hearts don’t stretch when camera moves)
        transform.localScale = initialScale;
    }
}
