using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private int damage = 10;
    private bool hasHit = false;
    public bool destroyAfterTime = true;
    void Start()
    {
        if (destroyAfterTime){
            this.GetComponent<CapsuleCollider2D>().enabled = true;
            StartCoroutine(DestroyAfterTime(5f));
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)  // For 2D physics
    {
        hasHit = true;
        // Optional: Destroy immediately on hit
        if (collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        } else {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
         
    }

    IEnumerator DestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!hasHit)
        {
            Destroy(gameObject);
        }
    }
}