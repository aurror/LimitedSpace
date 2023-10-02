using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public string message;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(true);
            FloatingLabelController.instance.SetText(message);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            FloatingLabelController.instance.ActivateLabe(false);
        }
    }
}
