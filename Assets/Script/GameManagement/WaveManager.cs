using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [HideInInspector] public bool waveActive = false;
    [HideInInspector] public int currentWave = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            waveActive = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        waveActive = true;

        Debug.Log("Wave " + waveNumber + " started");
    }

    public bool IsWaveActive(int enemyWave)
    {
        return waveActive && currentWave == enemyWave;
    }
}
