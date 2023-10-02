using System.Collections.Generic;
using UnityEngine;

public class StateManagerWeapons : MachineManager
{
    public List<TurretController> turrets = new List<TurretController>();
    private HealthState currentState = HealthState.Healthy;
    public override void OnHealthHealthy()
    {
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = Color.white;
            }
            
            turret.SetParams(0.42f, 180f);
        }
    }
    public override void OnHealthDamaged()
    {

    }
    public override void OnHealthBarelyOperable()
    {
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = new Color(1,1,0);
            }
            turret.SetParams(0.3f, 90f);
        }
    }
    public override void OnHealthFuckedUp()
    {
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = Color.red;
            }
            turret.SetParams(0f, 0f);
        }
    }
}