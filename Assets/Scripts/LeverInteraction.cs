using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    private Animator anim;
    private bool playerInRange;

    void Start()
    {
        playerInRange = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInRange)
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInRange)
            {
                
            }
        }
        if (playerInRange)
        {

        }
    }

    public void stopAnimation()
    {
        anim.ResetTrigger("LeverTrigger");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            FloatingLabelController.instance.ActivateLabe(false);
            FloatingLabelController.instance.SetInRange(false);
            anim.SetTrigger("LeverTrigger");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            // Assuming the trigger is a pickup item
            FloatingLabelController.instance.ActivateLabe(true);
            FloatingLabelController.instance.SetInRange(true);
        }
    }
}
