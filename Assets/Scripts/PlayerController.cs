using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] private float deathToleranceTime = 3f;

    public float JumpTimeFactor { get; set; }
    public float RunTimeFactor { get; set; }
    public float ClimbTimeFactor { get; set; }

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeet;
    private TimeController myTimeController;
    private float gravityScaleAtStart;
    private bool isAlive = true;
    private bool isRewinding = false;

    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myTimeController = FindObjectOfType<TimeController>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        RunTimeFactor = 1;
        JumpTimeFactor = 1;
        ClimbTimeFactor = 1;
    }

    void Update()
    {
        Rewind();
        Run();
        ClimbLadder();
        if (!isAlive || isRewinding) { return; }
        Jump();
        FlipSprite();
        Die();
    }

    private void Rewind() {
        var gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null) { return; }
        bool hasEnoughCoins = gameSession.HasCoins();
        if (hasEnoughCoins && Input.GetButtonDown("Rewind") && !isRewinding) {
            isRewinding = true;
            FindObjectOfType<GameSession>().SpendCoin();
            myTimeController.RewindTime(isRewinding);
            if (!isAlive) {
                myAnimator.SetTrigger("isRewinding");
            }
        }

        if (isRewinding && Input.GetButtonUp("Rewind")) {
             isRewinding = false;
             myTimeController.RewindTime(isRewinding);
        }
    }

    public void SetIsRewinding(bool isRewindingNow) {
        isRewinding = isRewindingNow;
    }

    public void StopRewinding() {
        isRewinding = false;
        myTimeController.RewindTime(false);
    }

    private void Run()
    {
        if (!isRewinding && isAlive) {
            float controlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed * RunTimeFactor, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    public void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        if (!isRewinding && isAlive) {
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed * ClimbTimeFactor);
            myRigidBody.velocity = climbVelocity;
        }

        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed * JumpTimeFactor);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Traps")))
        {
            StartCoroutine(OnDeath());
        }
    }

    private IEnumerator OnDeath() {
        isAlive = false;
        myAnimator.SetTrigger("isDying");
        GetComponent<Rigidbody2D>().velocity = deathKick;
        yield return new WaitForSeconds(deathToleranceTime);
        if (!isAlive) {
            FindObjectOfType<GameSession>().OnPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    public bool GetPlayerStatus() {
        return isAlive;
    }

    public void SetPLayerStatus(bool status) {
        isAlive = status;
    }

}