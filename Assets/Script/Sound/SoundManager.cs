using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;   // For SFX like shoot, win, die
    [SerializeField] private AudioSource musicSource; // Background music

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip dieSound;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Volumes")]
    [Range(0f, 2f)] [SerializeField] private float shootVolume = 1.2f;
    [Range(0f, 2f)] [SerializeField] private float winVolume = 1f;
    [Range(0f, 2f)] [SerializeField] private float dieVolume = 1f;
    [Range(0f, 1f)] [SerializeField] private float musicVolume = 0.4f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Background music setup
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }

        // Ensure SFX plays even if Time.timeScale = 0
        if (sfxSource != null)
            sfxSource.ignoreListenerPause = true;
    }

    // Play shooting sound
    public void PlayShoot() => sfxSource?.PlayOneShot(shootSound, shootVolume);

    // Play win sound and stop background music
    public void PlayWin()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        sfxSource?.PlayOneShot(winSound, winVolume);
    }

    // Play die sound and stop background music
    public void PlayDie()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        sfxSource?.PlayOneShot(dieSound, dieVolume);
    }

    // Optional: restart music after menu closes
    public void PlayBackgroundMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }
    public void SetMusicVolume(float volume)
{
    if (musicSource != null)
        musicSource.volume = Mathf.Clamp01(volume);
}

}
