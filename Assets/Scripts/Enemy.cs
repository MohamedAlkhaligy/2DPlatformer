using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] public float moveSpeed = 1f;


    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Bounce();
    }

    private void Bounce() {
        if (IsFacingRight()) {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight() {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D other) {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), transform.localScale.y);
    }

}
