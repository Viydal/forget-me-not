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
    private bool isMoving = false;
    private int activeCount = 0;

    private void Start() {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    public void Open() {
        // if (isOpen || isMoving) return;

        // AudioManager.instance.PlaySFX(AudioManager.instance.doorOpen);
        isMoving = true;
        isOpen = true;
    }

    public void Close() {
        // if (!isOpen) return;

        // AudioManager.instance.PlaySFX(AudioManager.instance.doorClose);
        isMoving = true;
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

    private void Update() {
        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * openSpeed);

        if (Vector3.Distance(transform.position, target) < 0.1f) {
            isMoving = false;
        }
    }
}
