using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinMenuController : MonoBehaviour
{
    public Transform fireball;
    public RectTransform[] menuPositions; // RESTART, MAIN MENU, QUIT
    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    [Header("UI Panels")]
    public GameObject winPanel;

    private int currentIndex = 0;

    void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        if (winPanel != null && winPanel.activeSelf)
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

    public void ShowWinMenuWithDelay(float delay = 0.5f)
    {
        StartCoroutine(ShowWinMenuCoroutine(delay));
    }

    private IEnumerator ShowWinMenuCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // unaffected by Time.timeScale

        Time.timeScale = 0f; // pause game
        if (winPanel != null)
            winPanel.SetActive(true);

        currentIndex = 0;
        UpdateFireballPosition();

        // Play win sound (background music stops in SoundManager)
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayWin();
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

    void SelectOption()
    {
        Time.timeScale = 1f; // resume time

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
                break;
        }
    }
}
