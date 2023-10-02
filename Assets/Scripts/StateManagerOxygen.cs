using UnityEngine;

public class StateManagerOxygen : MachineManager
{
    public PlayerHealth playerHealth;
    private HealthState currentState = HealthState.Healthy;
    public override void OnHealthHealthy()
    {
        playerHealth.StopSuffocating();
    }
    public override void OnHealthDamaged()
    {
        playerHealth.StopSuffocating();
    }
    public override void OnHealthBarelyOperable()
    {
        playerHealth.StopSuffocating();
    }
    public override void OnHealthFuckedUp()
    {
        playerHealth.StartSuffocating();
    }
}