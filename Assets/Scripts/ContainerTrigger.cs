using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTrigger : MonoBehaviour
{

    [SerializeField] private string resourceKind;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(false);
            FloatingLabelController.instance.SetInRange(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Assuming the trigger is a pickup item
            FloatingLabelController.instance.ActivateLabe(true);
            FloatingLabelController.instance.SetInRange(true);
            FloatingLabelController.instance.SetStringContainer(resourceKind);
        }
    }


 
}
