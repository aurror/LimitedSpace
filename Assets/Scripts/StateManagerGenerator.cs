using UnityEngine;

public class StateManagerGenerator : MachineManager
{
    [SerializeField] private GameObject mainLight;
    [SerializeField] private GameObject pointLight;


    

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

    public void TurnOnLight()
    {
        mainLight.SetActive(true);
        pointLight.SetActive(false);
    }

    public void TurnOfLight()
    {
        mainLight.SetActive(false);
        pointLight.SetActive(true);
    }
}