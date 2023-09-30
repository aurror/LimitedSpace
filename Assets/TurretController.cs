using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour
{
  
    public GameObject laserPrefab;  // Assign your laser prefab in the Inspector
    private float fireRate = 0.42f;  // Number of shots per second
    private float shotVelocity = 80f;
    private float lastShotTime;

    private bool canRotate = true;
    private float stopAfterShooting = 0.8f;
    private float rotationSpeed = 180.0f;
     private float firingAngleTolerance = 1f;  // Tolerance in degrees

    void Update()
    {
        if (Time.time >= lastShotTime + stopAfterShooting){
            RotateTowardsClosestEnemy();
        }
        
         AttemptToFireAtClosestEnemy();
    }
   void AttemptToFireAtClosestEnemy()
    {
        if (Time.time >= lastShotTime + 1f / fireRate)
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                Vector3 directionToTarget = closestEnemy.transform.position - transform.position;
                directionToTarget.z = 0;  // Ignore Z-axis differences
                float angle = Vector3.Angle(transform.up, directionToTarget);  // Assumes turret's forward direction is up

                if (angle < firingAngleTolerance)
                {
                    ShootLaserAt(closestEnemy.transform.position);
                    lastShotTime = Time.time;
                }
            }
        }
    }

    void ShootLaserAt(Vector3 targetPosition)
    {
Vector3 direction = (targetPosition - transform.position).normalized;
    Vector3 spawnPosition = transform.position + direction;  // Adjust as needed

    // Compute rotation to align laser's local Y-axis with direction
    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

    GameObject laser = Instantiate(laserPrefab, spawnPosition, rotation);
    laser.GetComponent<ProjectileController>().destroyAfterTime = true;
    Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.velocity = direction * shotVelocity;  // Adjust velocity as needed
    }
    }

    void RotateTowardsClosestEnemy()
    {
         GameObject closestEnemy = FindClosestEnemy();
    if (closestEnemy == null)
    {
        return;
    }

    Vector3 directionToTarget = closestEnemy.transform.position - transform.position;
    directionToTarget.z = 0; // Ignore Z-axis differences

    float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees to make the Y-axis face the enemy
    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float distanceSqrToEnemy = directionToEnemy.sqrMagnitude;
            if (distanceSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
