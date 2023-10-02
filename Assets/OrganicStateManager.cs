using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicStateManager : MachineManager
{
    private HealthState currentState = HealthState.Healthy;
    public override void OnHealthHealthy()
    {
        DamageManager.instance.StopOxygenDamage();
    }
    public override void OnHealthDamaged()
    {
        DamageManager.instance.StopOxygenDamage();
    }
    public override void OnHealthBarelyOperable()
    {
        DamageManager.instance.StopOxygenDamage();
    }
    public override void OnHealthFuckedUp()
    {
        DamageManager.instance.StartOxygenDamage();
    }

    public void SetPlayerSpeed(float newSpeet)
    {
        Movement.instance.speed = newSpeet;
    }

}
