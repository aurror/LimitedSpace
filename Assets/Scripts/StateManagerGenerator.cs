using UnityEngine;

public class StateManagerGenerator : MachineManager
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
}