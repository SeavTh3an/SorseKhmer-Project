using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health UI")]
    public SpriteRenderer[] hearts;       // Assign 3 heart SpriteRenderers
    public Sprite heartFull;              // Normal heart
    public Sprite heartBroken;            // Broken heart
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Player")]
    public Animator animator;             // Assign Player Animator
    public float disappearDelay = 1.5f;  // Time before player disappears

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
        // 1️⃣ Play death animation
        if (animator != null)
            animator.SetTrigger("die");

        // 2️⃣ Play die sound & stop background music
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayDie();

        // 3️⃣ Wait for disappear delay
        yield return new WaitForSeconds(disappearDelay);

        // 4️⃣ Hide player
        gameObject.SetActive(false);

        // 5️⃣ Show Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            // Optional: pause game
            Time.timeScale = 0f;
        }
    }
}
