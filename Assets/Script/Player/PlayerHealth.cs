using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer[] hearts;       // Assign 3 heart SpriteRenderers
    public Sprite heartFull;              // Normal heart
    public Sprite heartBroken;            // Broken heart
    public int maxHealth = 3;
    private int currentHealth;

    public Animator animator;             // Assign Player Animator
    public float disappearDelay = 1.5f;  // Time before player disappears after death

    [Header("Game Over")]
    public GameObject gameOverPanel;      // Assign your Game Over Panel here

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // Hide panel at start
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return; // Already dead

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? heartFull : heartBroken;
        }
    }

    IEnumerator Die()
    {
        // Play death animation
        if (animator != null)
            animator.SetTrigger("die");

        // Wait for disappear delay
        yield return new WaitForSeconds(disappearDelay);

        // Hide player
        gameObject.SetActive(false);

        // Show Game Over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
