using System.Collections;
using UnityEngine;

public class TimeSpeedController : MonoBehaviour
{
    [Tooltip("Minimum time till time speed changes in SECONDS")]
    [SerializeField] private float minOcurrenceRange = 5f;

    [Tooltip("Maximum time till time speed changes in SECONDS")]
    [SerializeField] private float maxOcurrenceRange = 10f;

    [Tooltip("Minimum time till time speed returns back to normal in SECONDS")]
    [SerializeField] private float minTimeRange = 1f;
    [Tooltip("Maximum time till time speed returns back to normal in SECONDS")]

    [SerializeField] private float maxTimeRange = 5f;
    [SerializeField] private float timeScale = 2f;
    [SerializeField] private GameObject postProcessingEffect;

    private bool isFinished = false;

    private IEnumerator Start()
    {
        while (!isFinished) {
            yield return new WaitForSeconds(Random.Range(minOcurrenceRange, maxOcurrenceRange));
            StartCoroutine(OnTimeScale());
        }
    }

    private IEnumerator OnTimeScale() {
        AdjustWorldSpeed(true);
        postProcessingEffect.SetActive(true);
        yield return new WaitForSeconds(Random.Range(minTimeRange, maxTimeRange));
        postProcessingEffect.SetActive(false);
        AdjustWorldSpeed(false);
    }

    private void AdjustWorldSpeed(bool isTimeChanged) {
        var enemies = FindObjectsOfType<Enemy>();
        if (isTimeChanged) {
            foreach(var enemy in enemies) {
                enemy.moveSpeed *= timeScale;
                enemy.GetComponent<Animator>().speed *= timeScale;
            }
            SoundManager.mySoundManager.SetSpeed(timeScale);
        } else {
            foreach(var enemy in enemies) {
                enemy.moveSpeed /= timeScale;
                enemy.GetComponent<Animator>().speed /= timeScale;
            }
            SoundManager.mySoundManager.SetSpeed(1);
        }
    }
}
