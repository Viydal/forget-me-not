using UnityEngine;

public class boxScript : MonoBehaviour {
    [SerializeField] private Rigidbody2D body;
    public Transform startTransform;

    public Vector2 intitalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        intitalPosition = startTransform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            body.position = intitalPosition;
            body.linearVelocity = Vector2.zero;
            body.rotation = 0;
        }
    }
}
