using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : MonoBehaviour
{
    private GameObject piratePrefab;
    public Transform ship;
    public float spawnRadius = 70f;  // Distance from ship at which asteroids spawn
    public float spawnRate = 3f;  // Spawns per second
    public float spawnRandomness = 0.5f;  // Randomness in spawn rate
   
    private float nextSpawnTime = 0f;

    private bool eventEnabled = false;
    private float remainingEventTime = 0f;
void Awake()
{
    piratePrefab = Resources.Load<GameObject>("Pirate");
}
    void Update()
    {
        
        if (eventEnabled)
        {
            remainingEventTime -= Time.deltaTime;
            if ( Time.time >= nextSpawnTime)
            {
                if (remainingEventTime <= 0f)
            {
                eventEnabled = false;
            }
            
            nextSpawnTime = Time.time + 1f / spawnRate + Random.Range(-spawnRandomness * 1f / spawnRate, spawnRandomness * 1f / spawnRate);
            SpawnPirate();
            }
            
        }
    }

    public void EnableEvent(float duration){
        this.remainingEventTime = duration;
        this.eventEnabled = true;
    }
    void SpawnPirate()
    {
        // Determine spawn position offscreen
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = ship.position + (Vector3)(spawnDirection * spawnRadius);

        // Create asteroid
        GameObject pirate = Instantiate(piratePrefab, spawnPosition, Quaternion.identity);
    }
}