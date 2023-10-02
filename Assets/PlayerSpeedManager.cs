using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedManager : MonoBehaviour
{
    [SerializeField] private GameObject organic;
    private float minSpeed = 3.5f;
    private float maxSpeed = 5f;

    private void Update()
    {
        float f = organic.GetComponent<FloatingHealthBar>().currentHealth;
        ChangePlayerSpeed(Mathf.Lerp(minSpeed, maxSpeed, f / 100f));
    }

    private void ChangePlayerSpeed(float newSpeed)
    {
        Movement.instance.speed = newSpeed;
    }
}
