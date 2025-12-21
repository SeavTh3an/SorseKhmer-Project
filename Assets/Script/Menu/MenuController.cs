using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Transform fireball;

    public RectTransform[] menuPositions; // PLAY, HELP, SETTING

    public Vector3 fireballOffset = new Vector3(-0.3f, 0f, 0f);

    [Header("UI Panels")]
    public GameObject menuPanel;         // MenuPanel reference
    public GameObject instructionPanel;  // InstructionPanel reference

    private int currentIndex = 0;        // Current menu selection

    void Start()
    {
        // Initial state
        menuPanel.SetActive(true);
        instructionPanel.SetActive(false);

        UpdateFireballPosition();
    }

    void Update()
    {
        if (menuPanel.activeSelf)
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
        else if (instructionPanel.activeSelf)
        {
            // Close instructions on any click/tap
            if (Input.GetMouseButtonDown(0))
            {
                CloseInstructions();
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
        switch (currentIndex)
        {
            case 0: // PLAY
                SceneManager.LoadScene("Level1Scene");
                break;

            case 1: // HELP
                ShowInstructions();
                break;

            case 2: // SETTING
                Debug.Log("Setting selected");
                break;
        }
    }

    void ShowInstructions()
    {
        menuPanel.SetActive(false);          // Hide menu buttons, player, enemy
        instructionPanel.SetActive(true);    // Show instruction panel
        // Fireball stays at last position (hidden behind menu panel)
    }

    void CloseInstructions()
    {
        instructionPanel.SetActive(false);   // Hide instructions
        menuPanel.SetActive(true);           // Show menu again
        UpdateFireballPosition();            // Ensure fireball appears at last selection
    }
}
