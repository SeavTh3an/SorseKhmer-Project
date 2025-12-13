using UnityEngine;

public class TypingManager : MonoBehaviour
{
    public Transform player;  // Drag your player transform here

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                EnemyTyping target = GetClosestEnemy();
                if (target != null)
                {
                    target.TypeLetter(c);
                }
            }
        }
    }

    // Find the closest enemy to the player
    EnemyTyping GetClosestEnemy()
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
