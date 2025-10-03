using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour {

    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 30f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        body.gravityScale = 6f;
    }

    // Update is called once per frame
     void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
        }
        
        if (body.linearVelocity.y > 0 || body.linearVelocity.y < 0) {
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isJumping", false);
        }
        Flip();
    }
    
    private void FixedUpdate() {
        body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
