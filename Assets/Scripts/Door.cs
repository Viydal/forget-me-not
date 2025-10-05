using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour {

    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private float openHeight = 3f;   // how far up the door should move
    [SerializeField] private float openSpeed = 2f;    // how fast it moves

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private int activeCount = 0;

    private void Start() {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    public void Open() {
        isOpen = true;
    }

    public void Close() {
        isOpen = false;
    }

    public void OnButton() {
        activeCount++;
        if (activeCount >= 2) {
            Open();
        }
    }

    public void OffButton() {
        activeCount--;
        Close();
    }

    public void SpecialButton() {
        AudioManager.instance.PlaySFX(AudioManager.instance.clap);
        Open();
    }

    private void Update() {
        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * openSpeed);
    }
}
