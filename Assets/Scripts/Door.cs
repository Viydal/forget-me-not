using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openHeight = 3f;   // how far up the door should move
    [SerializeField] private float openSpeed = 2f;    // how fast it moves

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    public void Open()
    {
        isOpen = true;
    }

    public void Close()
    {
        isOpen = false;
    }

    private void Update()
    {
        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * openSpeed);
    }
}
