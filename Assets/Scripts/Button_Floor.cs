using UnityEngine;
using UnityEngine.Events;

public class Button_Floor : MonoBehaviour
{
    [Header("Button Parameters")]
    [SerializeField] private Transform buttonTop;   // assign the top part of button
    [SerializeField] private Vector3 pressedOffset = new Vector3(0, -0.1f, 0); 
    [SerializeField] private float pressSpeed = 5f; 

    [SerializeField] private UnityEvent onPressed;   // event you can hook into
    [SerializeField] private UnityEvent onReleased;  // optional release event

    private Vector3 initialPosition;
    private bool isPressed = false;
    private int pressCount = 0; // track how many objects are on it

    private void Start()
    {
        if (buttonTop != null)
            initialPosition = buttonTop.localPosition;
    }

// Player steps on button
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // only respond to player
        {
            Debug.Log("Player stepped on button");
            pressCount++;
            if (!isPressed)
            {
                isPressed = true;
                onPressed.Invoke();
            }
        }
    }

//player steps off button
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressCount--;
            if (pressCount <= 0)
            {
                isPressed = false;
                onReleased.Invoke();
            }
        }
    }

    private void Update()
    {
        if (buttonTop == null) return;

        Vector3 targetPos = isPressed ? initialPosition + pressedOffset : initialPosition;
        buttonTop.localPosition = Vector3.Lerp(buttonTop.localPosition, targetPos, Time.deltaTime * pressSpeed);
    }
}
