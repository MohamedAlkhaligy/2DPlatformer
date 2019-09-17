using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource myAudioSource;

    public static SoundManager mySoundManager = null;

    void Start()
    {
        if (mySoundManager != null && mySoundManager != this) {
            Destroy(gameObject);
        } else {
            mySoundManager = this;
            myAudioSource = mySoundManager.GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnRewind() {
        //myAudioSource.timeSamples = myAudioSource.clip.samples - 1;
        myAudioSource.pitch = -1;
    }

    public void Resume() {
        //myAudioSource.timeSamples = myAudioSource.clip.samples + 1;
        myAudioSource.pitch = 1;
    }
    
    public void SetVolume(float volume) {
        myAudioSource.volume = volume;
    }

    public void SetSpeed(float amount) {
        myAudioSource.pitch =  amount;
    }
}
