using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using EZCameraShake;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;
    Portal activePortal;
    float startingGravity;
    int direction = 1;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(40f, 40f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    bool isAlive = true;


    void Start()
    {
        Debug.Log(activePortal);
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = playerRigidBody.gravityScale;

    }

    
    void Update()
    {
        bool isRunning = Mathf.Abs(playerRigidBody.velocity.x) > .01;
        // bool isRunning = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        
        if(isAlive)
        {
            Run(isRunning);
            ClimbLadder();
        }
        if(Keyboard.current.leftArrowKey.isPressed) {direction = -1;}
        if(Keyboard.current.rightArrowKey.isPressed) {direction = 1;}
        FlipSprite(direction);
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

    void OnFire(InputValue value)
    {
        if(isAlive)
        {
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

    void ClimbLadder()
    {
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x,  moveInput.y * climbSpeed);
            playerAnimator.SetBool("isClimbing", true);
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
    
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            HazardDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            Die();
        } 
        else if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Portals")))
        {
            activePortal = other.gameObject.GetComponent<Portal>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Portals"))
        {
            activePortal = null;
        }        
    }

    void OnPortalEnter(InputValue value)
    {
        if(activePortal is not null)
        {
            FindObjectOfType<GameSession>().FadeToLevel(activePortal.GetNextLevelName());
        }
    }

    void FlipSprite(int direction)
    {
        transform.localScale = new Vector2(direction, 1f);
    }

    void HazardDeath()
    {
        Die();
        playerRigidBody.velocity = deathKick;
        CameraShaker.Instance.ShakeOnce(4f, 4f , .1f, 1f);
    }

    void Die()
    {
        if(isAlive)
        {
            isAlive = false;
            playerRigidBody.velocity = new Vector2(0f, 0f);
            playerAnimator.SetTrigger("Death");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
