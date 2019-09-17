using UnityEngine;
using System.Collections;


public class TimeEntity : MonoBehaviour
{

    private Vector2 currentPosition, previousPosition;
    private Vector2 currentVelocity, previousVelocity;
    private Vector2 currentScale;


    private ArrayList keyFrames;
    private bool isRewinding = false;
    private bool isFirstRun = true;
    private bool status = true;
    private int frameCounter = 0;
    private int reverseFrameCounter = 0;
    private int timer = 0;
    private int keyFrameFactor;

    private const int MAX_TIME = 128;

    private void Start() {
        keyFrames = new ArrayList();
    }

    private void FixedUpdate() {
        if (!isRewinding) {
            timer = 0;
            if (frameCounter < 0) {
                frameCounter += 1;
            } else {
                frameCounter = 0;
                AddKeyFramesParameters();
            }
        } else {
            if (reverseFrameCounter > 0) {
                reverseFrameCounter -= 1;
            } else {
                reverseFrameCounter = 0;
                RestoreParameters();
            }

            if (isFirstRun) {
                isFirstRun = false;
                RestoreParameters();
            }

            InterpolateParameters();
        }

        if (keyFrames.Count > MAX_TIME) {
            keyFrames.RemoveAt(0);
        }
    }

    private void InterpolateParameters() {
        float interpolation = (float) reverseFrameCounter / (float) keyFrameFactor;
        transform.position = Vector2.Lerp(previousPosition, currentPosition, interpolation);
        GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(previousVelocity, currentVelocity, interpolation);
        transform.localScale = currentScale;
        var player = GetComponent<PlayerController>();
        if (player) {
            player.SetPLayerStatus(status);
        }
        // transform.localEulerAngles = Vector3.Lerp(previousRotation, currentRotation, interpolation);
    }

    public void SetInitialParameters(int newKeyFrameFactor) {
        keyFrameFactor = newKeyFrameFactor;
    }

    private void AddKeyFramesParameters() {
        bool isAlive = true;
        if (GetComponent<PlayerController>()) {
            isAlive = GetComponent<PlayerController>().GetPlayerStatus();
        }

        keyFrames.Add(new KeyFrame(transform.position, GetComponent<Rigidbody2D>().velocity,
                transform.localScale, isAlive));
    }

    public void SetRewindingTime(bool isRewinding) {
        this.isRewinding = isRewinding;
    }

    private void RestoreParameters()
    {
        int lastIndex = keyFrames.Count - 1;
        int secondToLastIndex = keyFrames.Count - 2;
     
        if(secondToLastIndex >= 0)
        {
            currentPosition  = (keyFrames[lastIndex] as KeyFrame).Position;
            previousPosition = (keyFrames[secondToLastIndex] as KeyFrame).Position;

            currentVelocity = (keyFrames[lastIndex] as KeyFrame).Velocity;
            previousVelocity = (keyFrames[secondToLastIndex] as KeyFrame).Velocity;

            currentScale = (keyFrames[lastIndex] as KeyFrame).Scale;
            status = (keyFrames[lastIndex] as KeyFrame).IsAlive;

            keyFrames.RemoveAt(lastIndex);
            timer += 1;
            var player = GetComponent<PlayerController>();
            if (player != null && timer >= MAX_TIME - 1) {
                player.StopRewinding();
            }
        }
    }
}
