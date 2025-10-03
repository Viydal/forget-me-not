using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Frame {
    public float time;
    public bool isFacingRight;
    public bool jump;

    public bool isJumping;
    public bool isRunning;

    public Vector2 position;
}

class PlayerRecorder : MonoBehaviour {

    [SerializeField] private ShadowReplay shadow;

    private float timer;
    public float recordInterval = 0.01f;

    public List<List<Frame>> recordings = new List<List<Frame>>();
    private List<Frame> currentRecording = new List<Frame>();

    private Movement movement;

    public bool isRecording = true;

    void Start() {
        movement = GetComponent<Movement>();
        StartRecording();
    }

    public void StartRecording() {
        currentRecording = new List<Frame>();
        isRecording = true;
        timer = 0f;
        Debug.Log("Recording started");
    }

    public void StopRecording() {
        recordings.Add(currentRecording);
        isRecording = false;
        movement.isDead = true;
        Debug.Log("Recording stopped - player dead");
    }

    void Update() {

        // Stop recording with R
        if (Input.GetKeyDown(KeyCode.R)) {
            StopRecording();
            shadow.BeginReplay(GetFrames());
            StartRecording();
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

            currentRecording.Add(frame);
        }
    }

    public List<Frame> GetFrames() {
        if (recordings.Count == 0) {
            return new List<Frame>();
        }
        return recordings[recordings.Count - 1];
    }
}