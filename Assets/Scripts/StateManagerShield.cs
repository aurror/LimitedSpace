using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StateManagerShield : MachineManager
{
    public GameObject outerShieldPrefab;
    public GameObject lightContainer; 


    public StateManagerWeapons weapons;

    public void VisualizeHealthLight(){
        var sprite = lightContainer.GetComponentInChildren<SpriteRenderer>();
        var light = lightContainer.GetComponentInChildren<Light2D>();
        HealthState lightState;
        if (currentHealthState >= weapons.currentHealthState){
            lightState = currentHealthState;
        } else {
            lightState = weapons.currentHealthState;
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
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.white;
        VisualizeHealthLight();
    }
    public override void OnHealthDamaged()
    {
        currentHealthState = HealthState.Damaged;
        VisualizeHealthLight();
    }
    public override void OnHealthBarelyOperable()
    {
        currentHealthState = HealthState.BarelyOperable;
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.red;
        VisualizeHealthLight();
    }
    public override void OnHealthFuckedUp()
    {
        currentHealthState = HealthState.FuckedUp;
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.clear;
        VisualizeHealthLight();
    }
}