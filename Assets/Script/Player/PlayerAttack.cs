using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
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

    // Called by TypingManager
    public void AttackEnemy(Transform enemy)
    {
        if (cooldownTimer < attackCooldown) return;
        if (playerMovement != null && !playerMovement.CanAttack()) return;

        anim?.SetTrigger("attack");
        cooldownTimer = 0f;

        // ✅ Tell SoundManager to play sound
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayShoot();
        }

        if (fireballs.Length == 0 || firePoint == null)
        {
            Debug.LogError("Fireballs or FirePoint not assigned!");
            return;
        }

        GameObject fireball = fireballs[FindFireball()];
        fireball.transform.position = firePoint.position;
        fireball.SetActive(true);

        Projectile proj = fireball.GetComponent<Projectile>();
        if (proj == null)
        {
            Debug.LogError("Projectile component missing!");
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
