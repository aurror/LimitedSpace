using UnityEngine;

public class StateManagerShield : MachineManager
{
    public GameObject outerShieldPrefab;
    private HealthState currentState = HealthState.Healthy;
    public override void OnHealthHealthy()
    {
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public override void OnHealthDamaged()
    {
        
    }
    public override void OnHealthBarelyOperable()
    {
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.red;
    }
    public override void OnHealthFuckedUp()
    {
        outerShieldPrefab.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}