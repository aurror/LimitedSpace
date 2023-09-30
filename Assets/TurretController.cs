using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour
{
  
    public GameObject laserPrefab;  // Assign your laser prefab in the Inspector
    private float fireRate = 0.42f;  // Number of shots per second
    private float shotVelocity = 50f;
    private float lastShotTime;


    void Update()
    {
        RotateTowardsClosestEnemy();
         ShootAtClosestEnemy();
    }
   void ShootAtClosestEnemy()
    {
        if (Time.time >= lastShotTime + 1f / fireRate)
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                ShootLaserAt(closestEnemy.transform.position);
                lastShotTime = Time.time;
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
        Transform target = closestEnemy.transform;
        if (closestEnemy != null)
        {
 Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.z = 0; // Ignore Z-axis differences

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 degrees to make the Y-axis face the enemy
        }
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
