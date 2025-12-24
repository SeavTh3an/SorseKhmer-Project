using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public Transform fireball;

    public RectTransform[] menuPositions; // RESTART, MAIN MENU, QUIT

    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    [Header("UI Panels")]
    public GameObject gameOverPanel; // GameOverPanel reference

    private int currentIndex = 0;     // Current menu selection

    void Start()
    {
        // Initial state
        gameOverPanel.SetActive(true);

        UpdateFireballPosition();
    }

    void Update()
    {
        if (gameOverPanel.activeSelf)
        {
            // Navigate down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex++;
                if (currentIndex >= menuPositions.Length)
                    currentIndex = 0;

                UpdateFireballPosition();
            }

            // Navigate up
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex--;
                if (currentIndex < 0)
                    currentIndex = menuPositions.Length - 1;

                UpdateFireballPosition();
            }

            // Confirm selection
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectOption();
            }
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
        Time.timeScale = 1f;
        switch (currentIndex)
        {
            case 0: // RESTART
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current level
                break;

            case 1: // MAIN MENU
                SceneManager.LoadScene("MenuScene"); // Replace with your main menu scene name
                break;

            case 2: // QUIT
                Application.Quit();
                Debug.Log("Quit game"); // Only visible in editor
                break;
        }
    }
}
