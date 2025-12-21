using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform firePoint;         // Assign empty child
    [SerializeField] private GameObject[] fireballs;      // Assign fireball prefabs

    private Animator anim;
    private PlayerMovement playerMovement;                // Optional
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    // Called by TypingManager when full word typed
    public void AttackEnemy(Transform enemy)
    {
        if (cooldownTimer < attackCooldown) return;
        if (playerMovement != null && !playerMovement.CanAttack()) return;

        anim?.SetTrigger("attack");
        cooldownTimer = 0;

        if (fireballs.Length == 0 || firePoint == null)
        {
            Debug.LogError("Fireballs array or FirePoint not assigned!");
            return;
        }

        GameObject fireball = fireballs[FindFireball()];
        fireball.transform.position = firePoint.position;

        Projectile proj = fireball.GetComponent<Projectile>();
        if (proj == null)
        {
            Debug.LogError("Projectile component missing on fireball prefab!");
            return;
        }

        proj.SetTarget(enemy);
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
