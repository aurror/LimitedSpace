using UnityEngine;

public class ShieldManager : MachineManager
{
    public override void OnHealthHealthy()
    {
        
    }
    public override void OnHealthDamaged()
    {
        Debug.Log("OH NO MY SHIELDS");
        // To be implemented in derived classes if needed
    }
}