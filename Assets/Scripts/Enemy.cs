
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

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
}
