using UnityEngine;
using System.Collections;
public class LevelExit : MonoBehaviour
{

    [SerializeField] private float exitDelay = 1.5f;

    private void OnTriggerEnter2D(Collider2D other) {
        bool isPlayerCharacter = other.GetComponent<PlayerController>();
        if (isPlayerCharacter) {
            StartCoroutine(OnExit());
        }
    }

    private IEnumerator OnExit() {
        Time.timeScale = 0.5f;
        var ScenePersist = FindObjectOfType<ScenePersist>();
        var timeController = FindObjectOfType<TimeController>();
        if (ScenePersist) { Destroy(ScenePersist.gameObject); }
        if (timeController) { Destroy(timeController.gameObject); }
        yield return new WaitForSeconds(exitDelay);
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().AddLevelPlayed();
        FindObjectOfType<LevelManager>().loadNextScene();
    }
}
