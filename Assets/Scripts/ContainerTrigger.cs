using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTrigger : MonoBehaviour
{

    [SerializeField] private string resourceKind;


    void OnTriggerEnter2D(Collider2D other)
    {
        // Assuming the trigger is a pickup item
        FloatingLabelController.instance.ActivateLabe(true);
        FloatingLabelController.instance.SetInRange(true);
        FloatingLabelController.instance.SetResource(resourceKind);
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        FloatingLabelController.instance.ActivateLabe(false);
        FloatingLabelController.instance.SetInRange(false);
    }
}