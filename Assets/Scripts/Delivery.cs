using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(105, 209, 19, 255);
    [SerializeField] Color32 missingPackageColor = new Color32(255, 255, 255, 255);
    [SerializeField] float destroyDelay = 0;
    SpriteRenderer spriteRenderer;
    bool hasPackage = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("BUMP");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        } 
        else if(other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Customer");
            hasPackage = false;
            spriteRenderer.color = missingPackageColor;
        }
    }
}
