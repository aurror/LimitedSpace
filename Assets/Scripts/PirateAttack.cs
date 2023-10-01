using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttack : MonoBehaviour
{
    [SerializeField] private Transform ship;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float orbitRadius = 4.0f;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float shootingInterval = 2.0f;
    [SerializeField] private GameObject gunPosition;
   // private Vector2 orbitPosition;
    private float currentAngle = 0.0f;

    private void Start()
    {
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

        // Ensure the angle stays within 360 degrees
        if (currentAngle >= 360.0f)
        {
            currentAngle -= 360.0f;
        }
       // transform.LookAt(ship);
        Vector2 directionToPlayer = (ship.position - gunPosition.transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            // Create a new projectile
            GameObject projectile = Instantiate(projectilePrefab, gunPosition.transform.position, Quaternion.identity);

            // Set the projectile's direction towards the player
            Vector2 directionToPlayer = (ship.position - transform.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * 5.0f;

            // Destroy the projectile after a certain time to avoid clutter
            Destroy(projectile, 2.0f);

            yield return new WaitForSeconds(shootingInterval);
        }
    }
}
