using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    private const int MAIN_MENU = 0;
    //private const int OPTIONS_MENU = 1;
    private const int FIRST_LEVEL = 1;
    private const int CREDITS_SCENE = 4;

    public void loadNextScene() {
        int curretnSceneIndex = GetCurrentSceneIndex();
        LoadScene(curretnSceneIndex + 1);
    }

    public void ReloadScene() {
        int curretnSceneIndex = GetCurrentSceneIndex();
        LoadScene(curretnSceneIndex);
    }

    public int GetCurrentSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMainMenu() {
        ResetPersistantData();
        LoadScene(MAIN_MENU);
    }

    private void ResetPersistantData() {
        var ScenePersist = FindObjectOfType<ScenePersist>();
        var timeController = FindObjectOfType<TimeController>();
        var gameSession = FindObjectOfType<GameSession>();
        if (ScenePersist) { Destroy(ScenePersist.gameObject); }
        if (timeController) { Destroy(timeController.gameObject); }
        if (gameSession) { Destroy(gameSession.gameObject); }
    }

    public void SkipLevel() {
        ResetPersistantData();
        loadNextScene();
    }

    // public void LoadOptionsMenu() {
    //     LoadScene(OPTIONS_MENU);
    // }

    public void LoadFirstLevel() {
        LoadScene(FIRST_LEVEL);
    }

    public void Quit() {
        Application.Quit();
    }

    private void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);        
    }
}
