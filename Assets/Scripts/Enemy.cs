
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    [SerializeField]
    public List<ResourceSpawner.ResourceType> resources = new List<ResourceSpawner.ResourceType>();
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Play death animation
        // Destroy enemy
        // spawn loot
        Destroy(gameObject);
    }

    public ResourceSpawner.ResourceType? GetRandomResource()
        {
        if (resources.Count > 0)
        {
            int randomIndex = Random.Range(0, resources.Count);
            ResourceSpawner.ResourceType randomResource = resources[randomIndex];
            return randomResource;
            // Now you can use randomResource
        }
        else
        {
            Debug.LogWarning("No resources available to select from.");
            return null;
        }
    }
}
