using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUI;

    private static bool isGamePaused = false;

    private void Awake() {
        pauseMenuUI.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            if (isGamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void Pause() {
        Time.timeScale = 0f;
        isGamePaused = true;
        pauseMenuUI.SetActive(true);
        FindObjectOfType<SoundManager>().SetVolume(0.5f);
    }

    public void LoadMainMenu() {
        Resume();
        FindObjectOfType<LevelManager>().LoadMainMenu();
    }

    public void SkipLevel() {
        Resume();
        FindObjectOfType<LevelManager>().SkipLevel();
    }

    public void Resume() {
        Time.timeScale = 1f;
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        FindObjectOfType<SoundManager>().SetVolume(1);
    }
}
