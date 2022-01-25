using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpSpeed = 10f;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        bool isRunning = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        Run(isRunning);
        FlipSprite(isRunning);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2d.velocity += new Vector2(0, jumpSpeed);
        }
    }

    void Run(bool isRunning)
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;
        playerAnimator.SetBool("isRunning", isRunning);
    }

    void FlipSprite(bool isRunning)
    {
        if(isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }
}
