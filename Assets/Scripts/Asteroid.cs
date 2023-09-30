using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Asteroid : Enemy
{
    public float lifeSpan = 10f;

    void Start()
    {
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
        // Optionally call base.Die() to include base class behavior
        base.Die();
        // Additional behavior specific to Asteroid death
    }
}