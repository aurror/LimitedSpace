using System.Collections.Generic;
using UnityEngine;

public class StateManagerWeapons : MachineManager
{
    public List<TurretController> turrets = new List<TurretController>();

    public StateManagerShield shield;


    public override void OnHealthHealthy()
    {
        currentHealthState = HealthState.Healthy;
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = Color.white;
            }
            
            turret.SetParams(0.42f, 180f);
        }
        shield.VisualizeHealthLight();
    }
    public override void OnHealthDamaged()
    {
        currentHealthState = HealthState.Damaged;
        shield.VisualizeHealthLight();
    }
    public override void OnHealthBarelyOperable()
    {
        currentHealthState = HealthState.BarelyOperable;
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = new Color(1,1,0);
            }
            turret.SetParams(0.3f, 90f);
        }
        shield.VisualizeHealthLight();
    }
    public override void OnHealthFuckedUp()
    {
        currentHealthState = HealthState.FuckedUp;
        foreach (var turret in turrets)
        {
            var turretComponents = turret.GetComponentsInChildren<SpriteRenderer>();
            foreach (var component in turretComponents)
            {
                component.color = Color.red;
            }
            turret.SetParams(0f, 0f);
        }
        shield.VisualizeHealthLight();
    }
}