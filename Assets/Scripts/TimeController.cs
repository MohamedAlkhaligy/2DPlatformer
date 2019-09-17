using UnityEngine;

public class TimeController : MonoBehaviour {

    [SerializeField] private TimeEntity[] entities;
    [SerializeField] private int keyFramesFactor = 5;
    [SerializeField] private GameObject postProcessingEffect;

    private static TimeController myTimeController =  null;

    private void Start() {
        if (myTimeController != null && myTimeController != this) {
            Destroy(gameObject);
        } else {
            myTimeController = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Initialize() {
        postProcessingEffect.SetActive(false);
        entities = FindObjectsOfType<TimeEntity>();
        foreach (var entity in entities) {
            entity.SetInitialParameters(keyFramesFactor);
        }
    }

    public void RewindTime(bool isRewinding) {
        if (isRewinding) {
            SetRewindForTimeEntities(true);
            FindObjectOfType<SoundManager>().OnRewind();
            postProcessingEffect.SetActive(true);
        } else {
            SetRewindForTimeEntities(false);
            FindObjectOfType<SoundManager>().Resume();
            postProcessingEffect.SetActive(false);
        }
    }

    private void SetRewindForTimeEntities(bool isRewinding) {
        foreach (var entity in entities) {
            entity.SetRewindingTime(isRewinding);
        }
    }
}