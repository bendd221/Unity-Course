using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using EZCameraShake;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    float startingGravity;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(40f, 40f);
    bool isAlive = true;
    
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = playerRigidBody.gravityScale;

    }

    
    void Update()
    {
        bool isRunning = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        bool isClimbing = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;

        if(isAlive)
        {
            Run(isRunning);
            ClimbLadder(isClimbing);
        }
        FlipSprite(isRunning);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(isAlive && value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidBody.velocity += new Vector2(0, jumpSpeed);
        }
    }

    void ClimbLadder(bool isClimbing)
    {
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x,  moveInput.y * climbSpeed);
                playerAnimator.SetBool("isClimbing", isClimbing);
        }
        else
        {
            playerRigidBody.gravityScale = startingGravity;
            playerAnimator.SetBool("isClimbing", false);
        }
    }

    void Run(bool isRunning)
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
        playerAnimator.SetBool("isRunning", isRunning);
    }

    void OnCollisionEnter2D() {
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            Die();
        }
    }
    void FlipSprite(bool isRunning)
    {
        if(isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }

    void Die()
    {
        isAlive = false;
        playerRigidBody.velocity = new Vector2(0f, 0f);
        playerAnimator.SetTrigger("Death");
        playerRigidBody.velocity = deathKick;
        CameraShaker.Instance.ShakeOnce(4f, 4f , .1f, 1f);
    }
}
