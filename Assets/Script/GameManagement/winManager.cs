using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private int aliveEnemies;

    public GameObject winPanel; // Panel with GameOverMenuController

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Count enemies automatically
        aliveEnemies = FindObjectsOfType<EnemyTyping>().Length;

        winPanel.SetActive(false);
    }

    public void EnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
{
    WinMenuController winMenu = FindObjectOfType<WinMenuController>();
    winMenu.ShowWinMenuWithDelay(3f); // 3-second delay before showing menu
    Debug.Log("YOU WIN!");
}
}
