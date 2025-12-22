using UnityEngine;
using TMPro;

public class EnemyTyping : MonoBehaviour
{
    public TMP_Text wordText;          // Assign TMP Text
    public string[] possibleWords;     // Khmer or English word list
    private string currentWord;
    private int currentCharIndex = 0;

    void Start()
    {
        GenerateNewWord();
    }

    void GenerateNewWord()
    {
        if (possibleWords.Length == 0) return;

        currentWord = possibleWords[Random.Range(0, possibleWords.Length)];
        wordText.text = currentWord;
        currentCharIndex = 0;
    }

    // Called by TypingManager for each typed letter
    public bool TypeLetter(char letter)
    {
        if (currentCharIndex >= currentWord.Length) return false;

        if (letter == currentWord[currentCharIndex])
        {
            currentCharIndex++;
            UpdateWordDisplay();

            if (currentCharIndex >= currentWord.Length)
            {
                return true; // Word completed
            }
        }
        return false;
    }

    void UpdateWordDisplay()
    {
        string typed = "<color=green>" + currentWord.Substring(0, currentCharIndex) + "</color>";
        string remaining = currentWord.Substring(currentCharIndex);
        wordText.text = typed + remaining;
    }

    // Called when projectile hits the enemy
    public void OnHitByFireball()
    {   
        EnemyManager.Instance.EnemyDied();
        // Optional: play death animation here
        Destroy(gameObject);
    }
}
