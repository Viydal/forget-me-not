using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 moveOffset = new Vector3(0, 3f, 0); // How far to move from start
    [SerializeField] private float moveSpeed = 2f;                        // Movement speed
    [SerializeField] private bool startOpen = false;                      // Start at target position?

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isActive = false;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;

        if (startOpen)
            transform.position = targetPosition;
    }

    private void Update()
    {
        Vector3 desired = isActive ? targetPosition : startPosition;
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * moveSpeed);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
