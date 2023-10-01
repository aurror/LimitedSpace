using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform ship;
    public float spawnRadius = 10f;  // Distance from ship at which asteroids spawn
    public float asteroidSpeed = 15f;
    public float asteroidSpeedRandomness = 0.8f;  // Randomness in asteroid speed
    public float spawnRate = 3f;  // Spawns per second
    public float spawnRandomness = 0.5f;  // Randomness in spawn rate
    public Vector2 shipVelocity = new Vector2(0f, 5f);  // Simulated ship velocity

    private float nextSpawnTime = 0f;

    private bool eventEnabled = false;
    private float remainingEventTime = 0f;
void Awake()
{
    asteroidPrefab = Resources.Load<GameObject>("Asteroid");
}
    void Update()
    {

        if (eventEnabled && Time.time >= nextSpawnTime)
        {
            if (remainingEventTime <= 0f)
            {
                eventEnabled = false;
            }
            remainingEventTime -= Time.deltaTime;
            nextSpawnTime = Time.time + 1f / spawnRate + Random.Range(-spawnRandomness * 1f / spawnRate, spawnRandomness * 1f / spawnRate);
            SpawnAsteroid();
        }
    }

    public void EnableEvent(float duration){
        this.remainingEventTime = duration;
        this.eventEnabled = true;
    }
    void SpawnAsteroid()
    {
 // Determine spawn position offscreen
    Vector2 spawnDirection = Random.insideUnitCircle.normalized;
    Vector3 spawnPosition = ship.position + (Vector3)(spawnDirection * spawnRadius);

    // Create asteroid
    GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

    // Calculate direction towards the bottom of the screen
    Vector2 directionToBottom = Vector2.down;

    // Calculate direction towards ship
    Vector2 directionToShip = ((Vector2)ship.position - (Vector2)spawnPosition).normalized;

    // Randomize a factor between 0 and 1 to blend between the two directions
    float blendFactor = Random.value;

    // Blend between the direction towards ship and direction towards bottom
    Vector2 blendedDirection = Vector2.Lerp(directionToShip, directionToBottom, blendFactor);

    // Adjust asteroid velocity to combine the blended movement direction
    // and the simulated ship velocity
    Vector2 asteroidVelocity = blendedDirection * (asteroidSpeed + Random.Range(asteroidSpeedRandomness * asteroidSpeed, -asteroidSpeedRandomness * asteroidSpeed)) - shipVelocity;

    // Set asteroid velocity
    asteroid.GetComponent<Rigidbody2D>().velocity = asteroidVelocity;
}
}