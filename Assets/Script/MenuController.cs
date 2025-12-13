using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Transform fireball;

    public RectTransform[] menuPositions; // PLAY, HELP, SETTING

    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    private int currentIndex = 0;

    void Start()
    {
        UpdateFireballPosition();
    }

    void Update()
    {
        // Move down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= menuPositions.Length)
                currentIndex = 0;

            UpdateFireballPosition();
        }

        // Move up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = menuPositions.Length - 1;

            UpdateFireballPosition();
        }

        // Confirm selection
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            SelectOption();
        }
    }

    void UpdateFireballPosition()
    {
        RectTransform target = menuPositions[currentIndex];

        float leftEdgeX = target.position.x - (target.rect.width * target.lossyScale.x) / 2f;

        Vector3 newPos = fireball.position;
        newPos.x = leftEdgeX + fireballOffset.x;
        newPos.y = target.position.y + fireballOffset.y;

        fireball.position = newPos;
    }

    void SelectOption()
    {
        switch (currentIndex)
        {
            case 0: // PLAY
                SceneManager.LoadScene("Level1Scene");
                break;

            case 1: // HELP
                Debug.Log("Help selected");
                break;

            case 2: // SETTING
                Debug.Log("Setting selected");
                break;
        }
    }
}
