using UnityEngine;

public class RewindTutorial : MonoBehaviour
{
    [SerializeField] private GameObject rewindText;

    private void Awake() {
        rewindText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var playerStatus = FindObjectOfType<GameSession>();
        bool playerHasCoins = playerStatus.HasCoins();
        if (playerHasCoins) {
            playerStatus.SpendCoin();
            FindObjectOfType<TimeController>().RewindTime(true);
            FindObjectOfType<PlayerController>().SetIsRewinding(true);
            rewindText.SetActive(true);
            Destroy(gameObject);
        }   
    }


}
