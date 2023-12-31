using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PirateAttack : Enemy
{
    private GameObject explosionPrefab;
    private Transform ship;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float orbitRadius = 4.0f;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float shootingInterval = 2.0f;
    [SerializeField] private GameObject gunPosition;

    private float itemSpawnChance = 2.3f;
   // private Vector2 orbitPosition;
    private float currentAngle = 0.0f;

    private void Start()
    {
         explosionPrefab = Resources.Load<GameObject>("Explosion");
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
        // Start a Coroutine to make enemies continuously shoot at the player
        StartCoroutine(ShootAtPlayer());
    }

    private void Update()
    {
        // Calculate the orbit position around the player using trigonometry
        float x = ship.position.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float y = ship.position.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;

        // Set the enemy's position to the calculated orbit position
        transform.position = new Vector2(x, y);

        // Rotate the enemy around the player
        currentAngle += rotationSpeed * Time.deltaTime;

        // Calculate the direction from the capsule's head to the target object
        Vector2 directionToTarget = ship.position - gunPosition.transform.position;

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Set the object's rotation to look at the target point while keeping the head aligned
        transform.rotation = Quaternion.Euler(0, 0, angle - 90.0f); // -90 degrees to adjust the initial orientation

        // Ensure the angle stays within 360 degrees
        if (currentAngle >= 360.0f)
        {
            currentAngle -= 360.0f;
        }
 
    }


    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            // Create a new projectile
            GameObject projectile = Instantiate(projectilePrefab, gunPosition.transform.position, Quaternion.identity * Quaternion.Euler(0, 0, 90));

            projectile.GetComponent<Rigidbody2D>().velocity = ship.position - gunPosition.transform.position * 0.5f;

            yield return new WaitForSeconds(shootingInterval);
        }
    }

    
    protected override void Die()
    {
        // Instantiate the explosion prefab at the asteroid's position and rotation
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Additional behavior specific to Asteroid death
        // spawn stuff
        var tmpSpawnChance = itemSpawnChance;
        while (tmpSpawnChance > 1f){
            tmpSpawnChance =- 1;
            // Spawn item
            var resource = this.GetRandomResource();
            if (resource != null){
                ResourceSpawner.instance.SpawnResource((ResourceSpawner.ResourceType)resource, transform.position, Vector2.zero);
            }
            
        }
        if (Random.value < itemSpawnChance)
        {
            // Spawn item
            var resource = GetRandomResource();
            if (resource != null){
                ResourceSpawner.instance.SpawnResource((ResourceSpawner.ResourceType)resource, transform.position, Vector2.zero);
            }
        }

        // Optionally call base.Die() to include base class behavior

        base.Die();
    }
}
