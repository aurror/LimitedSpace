using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StateManagerOxygen : MachineManager
{
    public PlayerHealth playerHealth;
    public GameObject lightContainer;
    public OrganicStateManager organic;

    public void VisualizeHealthLight(){
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        HealthState lightState;
        if (currentHealthState >= organic.currentHealthState){
            lightState = currentHealthState;
        } else {
            lightState = organic.currentHealthState;
        }
        switch (lightState)
        {
            case HealthState.Healthy:
                sprite.color = lightGreen;
                light.color = lightGreen;
                break;
            case HealthState.Damaged:
                sprite.color = lightYellow;
                light.color = lightYellow;
                break;
            case HealthState.BarelyOperable:
                sprite.color = lightRed;
                light.color = lightRed;
                break;
            case HealthState.FuckedUp:
                sprite.color = lightDead;
                light.color = lightDead;
                break;
        }
    }
    public override void OnHealthHealthy()
    {
        currentHealthState = HealthState.Healthy;
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightGreen;
        light.color = lightGreen;
        playerHealth.StopSuffocating();
        VisualizeHealthLight();
    }
    public override void OnHealthDamaged()
    {
        currentHealthState = HealthState.Damaged;
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightYellow;
        light.color = lightYellow;
        playerHealth.StopSuffocating();
        VisualizeHealthLight();
    }
    public override void OnHealthBarelyOperable()
    {
        currentHealthState = HealthState.BarelyOperable;
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightRed;
        light.color = lightRed;
        playerHealth.StopSuffocating();
        VisualizeHealthLight();
    }
    public override void OnHealthFuckedUp()
    {
        currentHealthState = HealthState.FuckedUp;
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        sprite.color = lightDead;
        light.color = lightDead;
        playerHealth.StartSuffocating();
        VisualizeHealthLight();
    }
}