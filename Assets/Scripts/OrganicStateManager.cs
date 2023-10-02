using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicStateManager : MachineManager
{
    private HealthState currentState = HealthState.Healthy;
    public StateManagerOxygen oxygen;
    public override void OnHealthHealthy()
    {
        currentHealthState = HealthState.Healthy;
        DamageManager.instance.StopOxygenDamage();
        oxygen.VisualizeHealthLight();
    }
    public override void OnHealthDamaged()
    {
        currentHealthState = HealthState.Damaged;
        DamageManager.instance.StopOxygenDamage();
        oxygen.VisualizeHealthLight();
    }
    public override void OnHealthBarelyOperable()
    {
        currentHealthState = HealthState.BarelyOperable;
        DamageManager.instance.StopOxygenDamage();
        oxygen.VisualizeHealthLight();
    }
    public override void OnHealthFuckedUp()
    {
        currentHealthState = HealthState.FuckedUp;
        DamageManager.instance.StartOxygenDamage();
        oxygen.VisualizeHealthLight();
    }

    public void SetPlayerSpeed(float newSpeet)
    {
        
        Movement.instance.speed = newSpeet;
    }

}
