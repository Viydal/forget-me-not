using System.Collections.Generic;
using UnityEngine;

class Frame {
    public float time;
    public bool isFacingRight;
    public bool jump;

    public bool isJumping;
    public bool isRunning;

    public Vector2 position;
}

class PlayerRecorder : MonoBehaviour {
    private float timer;
    public float recordInterval = 0.01f;

    private List<Frame> frames = new List<Frame>();

    private Movement movement;

    public bool isRecording = false;

    void Start() {
        movement = GetComponent<Movement>();
    }

    void Update() {

        // Start Recording with R
        if (Input.GetKeyDown(KeyCode.R)) {
            frames.Clear();
            timer = 0f;
            isRecording = true;
            Debug.Log("Recording started");
        }

        // Stop recording with T
        if (Input.GetKeyDown(KeyCode.T)) {
            isRecording = false;
            Debug.Log("Recording stopped");
        }

        if (!isRecording) return;

        // Capture inputs each frame
        timer += Time.deltaTime;
        if (timer >= recordInterval) {
            timer = 0f;

            Frame frame = new Frame();
            frame.isFacingRight = movement.isFacingRight;
            frame.time = Time.time;
            frame.jump = Input.GetKey(KeyCode.Space) && movement.IsGrounded();

            frame.isJumping = movement.isJumping;
            frame.isRunning = movement.isRunning;

            frame.position = transform.position;

            frames.Add(frame);
        }
    }

    public List<Frame> GetFrames() {
        return frames;
    }
}