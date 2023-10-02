using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    public float speed = 5f;  // Adjust the speed at which the character moves
    private float saveSpeed;
     private Rigidbody2D rb;
    public Animator animator;
    public GameObject DeathScreen;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        if (horizontal < 0f) { transform.localScale = new Vector2(-1f, 1f); }
        if (horizontal > 0f) { transform.localScale = new Vector2(1f, 1f); }

        animator.SetFloat("Horizontal",horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed",direction.sqrMagnitude);

        Vector2 velocity = direction * speed;
        rb.velocity = velocity;

        // Check if the "E" key is pressed.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Trigger the animation by setting a trigger parameter.
            animator.SetTrigger("Player_interact");
            // saveSpeed = speed;
            // speed = 0f;
        }
    }
    public void StopAnimation()
    {
        // Stop the animation by resetting the trigger.
        animator.ResetTrigger("Player_interact");
        // speed = saveSpeed;
    }

    public void startDeathScreen()
    {
        Debug.Log("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        DeathScreen.SetActive(true);
    }
}
