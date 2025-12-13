using UnityEngine;
using TMPro;

public class EnemyTyping : MonoBehaviour
{
    public TMP_Text wordText;          // Drag your WordText TMP here
    public string[] possibleWords;     // Khmer word list
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
        if (currentCharIndex >= currentWord.Length)
            return false;

        if (letter == currentWord[currentCharIndex])
        {
            currentCharIndex++;
            UpdateWordDisplay();

            if (currentCharIndex >= currentWord.Length)
            {
                DefeatEnemy();
                return true;
            }
        }
        return false;
    }

    void UpdateWordDisplay()
    {
        // Green typed letters + remaining white letters
        string typed = "<color=green>" + currentWord.Substring(0, currentCharIndex) + "</color>";
        string remaining = currentWord.Substring(currentCharIndex);
        wordText.text = typed + remaining;
    }

    void DefeatEnemy()
    {
        // Optional: play death animation here
        Destroy(gameObject);
    }
}
