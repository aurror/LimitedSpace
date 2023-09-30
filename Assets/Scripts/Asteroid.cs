using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Asteroid : Enemy
{
      public GameObject explosionPrefab;
    public float lifeSpan = 10f;
    private float itemSpawnChance = 0.5f;

    void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Explosion");
        // Set the asteroid's health
        this.health = 10;
        Invoke("DespawnAsteroid", lifeSpan);
    }

    void DespawnAsteroid()
    {
        Destroy(gameObject);
    }

    // Override the Die method if you want special behavior for asteroids
    protected override void Die()
    {
        // Instantiate the explosion prefab at the asteroid's position and rotation
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Additional behavior specific to Asteroid death
        // spawn stuff
        if (Random.value < itemSpawnChance)
        {
            // Spawn item
            ResourceSpawner.instance.SpawnResource(ResourceSpawner.ResourceType.Iron, transform.position, GetComponent<Rigidbody2D>().velocity);
        }

        // Optionally call base.Die() to include base class behavior

        base.Die();
    }
}