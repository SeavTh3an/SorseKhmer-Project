using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shootSound;

    [Header("Shoot Sound Volume ONLY")]
    [Range(0f, 2f)]
    [SerializeField] private float shootVolume = 1.2f; // >1 = louder

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 🔊 Shooting sound (ONLY this is affected)
    public void PlayShoot()
    {
        if (shootSound == null || sfxSource == null) return;

        sfxSource.PlayOneShot(shootSound, shootVolume);
    }
}
