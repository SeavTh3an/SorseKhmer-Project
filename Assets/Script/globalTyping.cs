using UnityEngine;
using System.Collections;

public class TypingManager : MonoBehaviour
{
    public Transform player;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    [SerializeField] private float moveDelay = 0.5f; // Delay before player can move again

    private bool isTyping = false;
    private EnemyTyping currentTarget;

    void Update()
    {
        // Start typing with Enter
        if (!isTyping && Input.GetKeyDown(KeyCode.Return))
        {
            currentTarget = GetClosestEnemy();
            if (currentTarget != null)
            {
                isTyping = true;
                playerMovement.canMove = false; // stop player
            }
        }

        if (isTyping)
        {
            foreach (char c in Input.inputString)
            {
                if (currentTarget == null) break;

                bool completed = currentTarget.TypeLetter(c);

                if (completed)
                {
                    playerAttack.AttackEnemy(currentTarget.transform);

                    isTyping = false;
                    currentTarget = null;

                    // Start delay coroutine before allowing movement
                    StartCoroutine(EnableMovementAfterDelay());
                    break;
                }
            }

            // Cancel typing
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isTyping = false;
                playerMovement.canMove = true;
                currentTarget = null;
            }
        }
    }

    private IEnumerator EnableMovementAfterDelay()
    {
        yield return new WaitForSeconds(moveDelay); // Wait a bit
        playerMovement.canMove = true; // Enable movement
    }

    private EnemyTyping GetClosestEnemy()
    {
        EnemyTyping[] enemies = FindObjectsOfType<EnemyTyping>();
        EnemyTyping closest = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, player.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}
