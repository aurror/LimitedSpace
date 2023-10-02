using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision with Lever");
        anim.SetTrigger("LeverTrigger");
    }

    public void stopAnimation()
    {
        anim.ResetTrigger("LeverTrigger");
    }
}
