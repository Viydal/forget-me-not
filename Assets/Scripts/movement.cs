using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {

    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 30f;
    public bool isFacingRight = true;

    public bool isJumping = false;
    public bool isRunning = false;

    public Transform startTransform;

    public Vector2 inititalPosition;

    public bool isDead = false;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator animator;
    private bool isWalkSoundPlaying = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        body.gravityScale = 6f;
        inititalPosition = startTransform.position;
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0 && IsGrounded()) {
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
            if (!isWalkSoundPlaying) {
                AudioManager.instance.PlayLoopingSFX(AudioManager.instance.walk);
                isWalkSoundPlaying = true;
            }

        } else {
            isRunning = false;
            animator.SetBool("isRunning", isRunning);
            if (isWalkSoundPlaying) {
                AudioManager.instance.StopLoopingSFX();
                isWalkSoundPlaying = false;
            }
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
        }

        if (IsGrounded()) {
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
        } else {
            isJumping = true;
            animator.SetBool("isJumping", isJumping);
        }

        if (isDead) {
            body.position = inititalPosition;
            body.linearVelocity = Vector2.zero;
            isDead = !isDead;
        }

        Flip();
    }

    private void FixedUpdate() {
        body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);
    }

    public bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}