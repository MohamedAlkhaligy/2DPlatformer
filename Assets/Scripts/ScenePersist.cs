using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private int curretSceneIndexStatic;
    private static ScenePersist scenePersist = null;

    private void Awake() {
        if (scenePersist != null && scenePersist != this) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            scenePersist = this;
        }
    }

    private void Start() {
        curretSceneIndexStatic = FindObjectOfType<LevelManager>().GetCurrentSceneIndex();
    }

    private void Update() {
        int curretSceneIndex = FindObjectOfType<LevelManager>().GetCurrentSceneIndex();
        if (curretSceneIndex != curretSceneIndexStatic) {
            Destroy(gameObject);
        }
    }
}
