using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    
    [SerializeField] float steerSpeed = 250f;
    [SerializeField] float moveSpeed = 20f;
    void Start()
    {
        
    }

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        if(Input.GetKey(KeyCode.DownArrow)) 
        {
            Debug.Log("test");
            transform.Rotate(0, 0, steerAmount);
        } else 
        {
            transform.Rotate(0, 0, -steerAmount);
        }
        transform.Translate(0, moveAmount, 0);
    }
}
