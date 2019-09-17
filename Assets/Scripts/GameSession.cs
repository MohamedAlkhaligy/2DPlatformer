using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] private int playerLives = 3;
    [SerializeField] private Text liveCounterText;
    [SerializeField] private Text scoreCounterText;

    private static GameSession gameSession = null;
    private int coins = 0;
    private int numberOfLevelsPlayed = 0;

    private void Awake() {
        if (gameSession != null && gameSession != this) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            gameSession = this;
        }
    }
    
    public static GameSession GetGameSession() {
        return gameSession;
    }

    private void Start() {
        liveCounterText.text = playerLives.ToString();
        scoreCounterText.text = coins.ToString();
    }

    public void OnPlayerDeath() {
        if (playerLives > 1) {
            TakeLife();
        } else {
            ResetGameSession();
        }
    }

    public void PickUpCoin(int coinValue) {
        coins += coinValue;
        scoreCounterText.text = coins.ToString();
    }

    private void TakeLife() {
        playerLives -= 1;
        liveCounterText.text = playerLives.ToString();
        FindObjectOfType<LevelManager>().ReloadScene();
    }

    public bool HasCoins() {
        if (coins <= 0) return false;
        return true;
    }

    public void SpendCoin() {
        if (HasCoins()) {
            coins -= 1;
            scoreCounterText.text = coins.ToString();
        }
    }

    public int GetCoins() {
        return coins;
    }

    public int GetLevelsPlayed() {
        return numberOfLevelsPlayed;
    }


    public void AddLevelPlayed() {
        numberOfLevelsPlayed++;
    }

    private void ResetGameSession() {
        FindObjectOfType<LevelManager>().LoadMainMenu();
        FindObjectOfType<PlayerController>().SetPLayerStatus(true);
        Destroy(gameObject);
    }
}
