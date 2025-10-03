using NUnit;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class ShadowReplay : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerRecorder record;
    private float timer = 0f;
    private int currentIndex = 0;

    public bool beginReplay = false;

    private List<Frame> frames;

    void Start() {
        body.bodyType = RigidbodyType2D.Kinematic;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color colour = sr.color;
        colour.a = 0.5f;
        sr.color = colour;
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void BeginReplay(List<Frame> recordingFrames) {
        if (record.recordings.Count == 0) {
            gameObject.GetComponent<Renderer>().enabled = false;
        } else {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        frames = new List<Frame>(recordingFrames);
        currentIndex = 0;
        beginReplay = true;
        Debug.Log(frames.Count);
    }

    // Update is called once per frame
    void Update() {

        if (!beginReplay) return;

        if (currentIndex >= frames.Count) {
            beginReplay = false;
            return;
        }

        // Fetch frames dynamically if needed
        if (frames == null || frames.Count == 0) {
            frames = record.GetFrames();
            currentIndex = 0;
            if (frames == null || frames.Count == 0) return;
        }

        // Safety check
        if (currentIndex >= frames.Count) {
            currentIndex = 0; // loop replay
        }
        // Debug.Log("frame count: " + frames.Count);
        float recordInterval = record.recordInterval;

        timer += Time.deltaTime;
        if (timer >= recordInterval) {
            Frame frame = frames[currentIndex];

            bool isFacingRight = frame.isFacingRight;
            Flip(body, isFacingRight);

            body.MovePosition(frame.position);

            animator.SetBool("isRunning", frame.isRunning);
            animator.SetBool("isJumping", frame.isJumping);

            currentIndex++;
            timer = 0f;
        }

        if (currentIndex >= frames.Count) {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
        }
    }

    public void Flip(Rigidbody2D body, bool isFacingRight) {
        Vector3 localScale = body.transform.localScale;
        if (!isFacingRight) {
            localScale.x = -1f;
            body.transform.localScale = localScale;
        } else {
            localScale.x = 1f;
            body.transform.localScale = localScale;
        }
    }
}