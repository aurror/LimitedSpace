using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamCollector : MonoBehaviour
{
    public float collectionRadius = 5f;
    public float collectionSpeed = 5f;
    private Vector3 screenCenter;

    void Start()
    {
        // Assuming the center of the screen is the target point for collection
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
    }

    void Update()
    {
        CollectResources();
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
                // Move the resources towards the position of this GameObject
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, collectionSpeed * Time.deltaTime);

                // Check if the resource is close enough to be considered collected
                if (Vector3.Distance(collider.transform.position, transform.position) < 0.1f)
                {
                    // Destroy the resource
                    Destroy(collider.gameObject);
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
