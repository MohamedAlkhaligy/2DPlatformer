using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    [SerializeField] private Text scoreText;

    private GameSession myGameSession;

    void Start()
    {
        myGameSession = GameSession.GetGameSession();
        scoreText.text = "0";
        if (myGameSession) {
            var score = (myGameSession.GetCoins() + 1) * myGameSession.GetLevelsPlayed();
            scoreText.text = score.ToString();
        }
    }
}
