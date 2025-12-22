using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinMenuController : MonoBehaviour
{
    public Transform fireball;
    public RectTransform[] menuPositions; // RESTART, MAIN MENU, QUIT
    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    [Header("UI Panels")]
    public GameObject winPanel; // Panel that shows when player wins

    private int currentIndex = 0;

    void Start()
    {
        winPanel.SetActive(false); // Initially hidden
    }

    void Update()
    {
        if (winPanel.activeSelf)
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

    public void ShowWinMenuWithDelay(float delay = 3f)
    {
        StartCoroutine(ShowWinMenuCoroutine(delay));
    }

    private IEnumerator ShowWinMenuCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f; // Pause game
        winPanel.SetActive(true);
        currentIndex = 0;
        UpdateFireballPosition();
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
        // Reset Time.timeScale before loading new scene
        Time.timeScale = 1f;

        switch (currentIndex)
        {
            case 0: // RESTART
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case 1: // MAIN MENU
                SceneManager.LoadScene("MenuScene");
                break;

            case 2: // QUIT
                Application.Quit();
                Debug.Log("Quit game");
                break;
        }
    }
}
