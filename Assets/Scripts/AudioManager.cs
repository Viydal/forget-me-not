using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Audio source")]
    public static AudioManager instance;
    [SerializeField] AudioSource musicSource;
    public float musicVolume = 0.04f;
    [SerializeField] AudioSource SFXSource;
    public float SFXVolume = 1;
    [SerializeField] AudioSource loopingSFXSource;

    [Header("Audio clip")]
    public AudioClip background;
    public AudioClip menuButtonHover;
    public AudioClip menuButtonSelect;
    public AudioClip death;
    public AudioClip ghostDeath;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip wallTouch;
    public AudioClip finishLevel;
    public AudioClip buttonActive;
    public AudioClip buttonInactive;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioClip collectSoul;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.clip = background;
            SetMusicVolume(musicVolume);
            musicSource.Play();

            SetSFXVolume(SFXVolume);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX() {
        SFXSource.Stop();
    }

    public void PlayLoopingSFX(AudioClip clip) {
        if (loopingSFXSource.clip != clip || !loopingSFXSource.isPlaying) {
            loopingSFXSource.clip = clip;
            loopingSFXSource.loop = true;
            loopingSFXSource.Play();
        }
    }

    public void StopLoopingSFX() {
        loopingSFXSource.Stop();
        loopingSFXSource.clip = null;
    }

    public void SetSFXVolume(float volume) {
        Debug.Log("SFX volume changed to: " + volume);
        SFXSource.volume = volume;
    }

    public void SetMusicVolume(float volume) {
        Debug.Log("Music volume changed to: " + volume);
        musicSource.volume = volume;
    }
}
