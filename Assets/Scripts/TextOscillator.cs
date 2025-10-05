using UnityEngine;
using UnityEngine.UI;

public class TextOscillator : MonoBehaviour {
    public Image image;
    public float amplitude = 0.02f;
    public float speed = 1;

    private Vector3 startScale;

    void Start() {
        startScale = transform.localScale;
    }

    void Update() {
        float grow = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localScale = startScale * (1 + grow);
    }
}
