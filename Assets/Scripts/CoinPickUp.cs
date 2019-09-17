using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    [SerializeField] private int coinValue = 1;
    [SerializeField] private AudioClip coinPickUpSFX;

    private int counter = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        var isPlayerTouching = other.GetComponent<PlayerController>();
        if (isPlayerTouching && counter == 0) {
            counter++;
            FindObjectOfType<GameSession>().PickUpCoin(coinValue);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
