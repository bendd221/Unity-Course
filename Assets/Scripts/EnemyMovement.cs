using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    BoxCollider2D enemyBoxCollider;
    [SerializeField] float moveSpeed = 2f;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2 (moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)  
    {

        if(other.gameObject.layer != LayerMask.NameToLayer("Hazards"))
        {
            FlipEnemy();
            moveSpeed = -moveSpeed;   
        }
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
}
