using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StateManagerOxygen : MachineManager
{
    public PlayerHealth playerHealth;
    public GameObject lightContainer;
    private HealthState currentState = HealthState.Healthy;
    public override void OnHealthHealthy()
    {
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightGreen;
        light.color = lightGreen;
        playerHealth.StopSuffocating();
    }
    public override void OnHealthDamaged()
    {
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightYellow;
        light.color = lightYellow;
        playerHealth.StopSuffocating();
    }
    public override void OnHealthBarelyOperable()
    {
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightRed;
        light.color = lightRed;
        playerHealth.StopSuffocating();
    }
    public override void OnHealthFuckedUp()
    {
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightDead;
        light.color = lightDead;
        playerHealth.StartSuffocating();
    }
}