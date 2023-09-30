using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    public float speed = 5f;  // Adjust the speed at which the character moves
     private Rigidbody2D rb;

 void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
         float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        
        Vector2 velocity = direction * speed;
        rb.velocity = velocity;
    }
}
