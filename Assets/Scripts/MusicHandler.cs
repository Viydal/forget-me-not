using UnityEngine;

public class MusicHandler : MonoBehaviour {
    private static MusicHandler instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        } else {
            Destroy(gameObject);
        }
    }
}
