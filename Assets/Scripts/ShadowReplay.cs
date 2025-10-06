using NUnit;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class ShadowReplay : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerRecorder record;
    [SerializeField] private Ghost ghost;
    [SerializeField] private SpriteRenderer sprite;
    private float timer = 0f;
    private int currentIndex = 0;

    public bool beginReplay = false;

    private List<Frame> frames;

    void Start() {
        sprite.enabled = false;
        body.bodyType = RigidbodyType2D.Kinematic;
        Color colour = sprite.color;
        colour.a = 0.5f;
        sprite.color = colour;
    }

    public void BeginReplay(List<Frame> recordingFrames) {
        if (record.recordings.Count == 0) {
            sprite.enabled = false;
            return;
        }

        frames = new List<Frame>(recordingFrames);
        currentIndex = 0;

        Frame firstFrame = frames[0];

        transform.position = firstFrame.position;
        body.position = firstFrame.position;

        Flip(body, firstFrame.isFacingRight);
        animator.SetBool("isRunning", firstFrame.isRunning);
        animator.SetBool("isJumping", firstFrame.isJumping);

        timer = 0f;
        beginReplay = true;
        sprite.enabled = true;

        currentIndex = 1;
    }


    void Update() {
        if (GameManager.Instance.isPaused) {
            body.linearVelocity = Vector2.zero;
            AudioManager.instance.StopLoopingSFX();
            body.bodyType = RigidbodyType2D.Kinematic;
            animator.speed = 0f;
            return;
        } else {
            animator.speed = 1f;
            body.bodyType = RigidbodyType2D.Dynamic;
        }

        if (!beginReplay) return;
        animator.SetBool("isDead", false);

        if (currentIndex >= frames.Count) {
            beginReplay = false;
            return;
        }

        if (frames == null || frames.Count == 0) {
            frames = record.GetFrames();
            currentIndex = 0;
            if (frames == null || frames.Count == 0) return;
        }

        if (currentIndex >= frames.Count - 1) {
            AudioManager.instance.PlaySFX(AudioManager.instance.ghostDeath);
            ghost.StartFade();
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isDead", true);
            beginReplay = false;
            return;
        }

        float recordInterval = record.recordInterval;

        Frame currentFrame = frames[currentIndex];
        Frame nextFrame = frames[currentIndex + 1];

        float t = timer / recordInterval;

        Vector2 smoothPosition = Vector2.Lerp(currentFrame.position, nextFrame.position, t);
        body.MovePosition(smoothPosition);

        animator.SetBool("isRunning", currentFrame.isRunning);
        animator.SetBool("isJumping", currentFrame.isJumping);
        Flip(body, currentFrame.isFacingRight);

        timer += Time.deltaTime;

        if (timer >= recordInterval) {
            currentIndex++;
            timer = 0f;
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