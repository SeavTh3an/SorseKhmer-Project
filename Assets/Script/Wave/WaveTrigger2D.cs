using UnityEngine;
using TMPro;
using System.Collections;

public class WaveTrigger : MonoBehaviour
{
    public int waveNumber = 1;
    public TextMeshProUGUI waveText;
    public float displayDuration = 2f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            WaveManager.Instance.StartWave(waveNumber);
            StartCoroutine(ShowWaveText());
        }
    }

    IEnumerator ShowWaveText()
    {
        waveText.text = "WAVE " + waveNumber;
        waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        waveText.gameObject.SetActive(false);
    }
}
