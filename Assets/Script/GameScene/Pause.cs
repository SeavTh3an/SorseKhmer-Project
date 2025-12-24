using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pausePanel;            // Assign your Pause Panel
    public Transform fireball;               // Fireball pointer
    public RectTransform[] menuPositions;    // Continue, Restart, Main Menu, Quit
    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    private int currentIndex = 0;            // Selected menu index
    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);    // Hide panel at start
    }

    void Update()
    {
        // Toggle pause with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Handle menu navigation when paused
        if (isPaused && pausePanel != null && menuPositions.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex + 1) % menuPositions.Length;
                UpdateFireballPosition();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex--;
                if (currentIndex < 0) currentIndex = menuPositions.Length - 1;
                UpdateFireballPosition();
            }

            if (Input.GetKeyDown(KeyCode.Return))
                SelectOption();
        }
    }

    void UpdateFireballPosition()
    {
        if (fireball == null || menuPositions.Length == 0) return;

        RectTransform target = menuPositions[currentIndex];
        float leftEdgeX = target.position.x - (target.rect.width * target.lossyScale.x) / 2f;

        Vector3 newPos = fireball.position;
        newPos.x = leftEdgeX + fireballOffset.x;
        newPos.y = target.position.y + fireballOffset.y;

        fireball.position = newPos;
    }

    void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;  // Freeze the game
        isPaused = true;

        currentIndex = 0;
        UpdateFireballPosition();

        // Disable player movement
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
            player.canMove = false;

        // Optionally lower music volume
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(0.1f);
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;  // Unfreeze the game
        isPaused = false;

        // Enable player movement again
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
            player.canMove = true;

        // Restore music volume
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(0.4f);
    }

    void SelectOption()
    {   
        Time.timeScale = 1f;
        switch (currentIndex)
        {
            case 0: // Continue
                ResumeGame();
                break;

            case 1: // Restart
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current level
                break;
            case 2: // Main Menu
                Application.Quit();
                break;
        }
    }
}
