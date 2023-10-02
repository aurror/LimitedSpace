using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamCollector : MonoBehaviour
{
    private float collectionRadius = 80f;
    private float collectionSpeed = 2f;
    private Vector3 screenCenter;

    private bool isWorking = false;
    void Start()
    {
        // Assuming the center of the screen is the target point for collection
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
    }

    void Update()
    {
        if (ContainerManager.instance.currentResourceAmount < ContainerManager.instance.maxRessourceAmount){
            if (!isWorking){
                isWorking = true;
                gameObject.transform.parent.GetComponent<Animator>().enabled = true;
            }
            CollectResources();
        } else {
            if (isWorking){
                
                gameObject.transform.parent.GetComponent<Animator>().enabled = false;
                gameObject.transform.localScale = new Vector3(1.5f,1.5f,1);
                isWorking = false;
            }      
        }
    }

void CollectResources()
{
    // Get all colliders within the collection radius
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collectionRadius);

    foreach (Collider2D collider in colliders)
    {
        // Check if the collider has the FloatingResource tag
        if (collider.CompareTag("FloatingResource"))
        {
            // Get the Rigidbody2D component of the resource
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate the direction towards the center point (this GameObject)
                Vector2 direction = ((Vector2)transform.position - rb.position).normalized;

                // Apply a strong force to quickly change the trajectory of the resource
                float forceMagnitude = collectionSpeed * 10;  // Adjust multiplier as needed
                rb.AddForce(direction * forceMagnitude, ForceMode2D.Force);
                
                // Dampen the velocity to control the speed
                float damping = 0.2f;  // Adjust damping factor as needed (between 0 and 1, where 0 is no damping and 1 is full damping)
                rb.velocity = Vector2.Lerp(rb.velocity, direction * collectionSpeed, damping * Time.deltaTime);

                // Check if the resource is close enough to be considered collected
                float distance = Vector2.Distance(transform.position, rb.position);
                if (distance < 0.5f)
                {
                    // Collect the resource
                    ContainerManager.instance.GetNewResourceInContainer(collider.gameObject.GetComponent<FloatingResource>().resourceType.ToString(), 1);
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
    void OnDrawGizmos()
    {
        // Draw a red sphere at the position of the GameObject, using the collection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collectionRadius);
    }
}
