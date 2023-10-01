using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirareProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f; // Speed of the projectile
    [SerializeField] private float lifetime = 2.0f; // Time in seconds before the projectile is destroyed

    private void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collisions with other objects
        // You can implement custom behavior here, e.g., damaging an enemy on collision
        if (other.CompareTag("Ship"))
        {
            // Handle collision with the ship
            // Example: Deal damage to the enemy
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
