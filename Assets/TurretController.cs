using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    void Update()
    {
        RotateTowardsClosestEnemy();
    }

    void RotateTowardsClosestEnemy()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToEnemy.z, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
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
