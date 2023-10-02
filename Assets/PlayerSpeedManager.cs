using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedManager : MonoBehaviour
{
    [SerializeField] private GameObject organic;

    private void Update()
    {
        float f = organic.GetComponent<FloatingHealthBar>().currentHealth;
        ChangePlayerSpeed(f/100 * 2.5f + 2.5f);
    }

    private void ChangePlayerSpeed(float newSpeed)
    {
        Movement.instance.speed = newSpeed;
    }
}
