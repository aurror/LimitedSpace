using UnityEngine;

public enum HealthState {
    Healthy,
    Damaged,
    BarelyOperable,
    FuckedUp,
}
// Base class for all destroyable machines
public class MachineManager : MonoBehaviour
{

    protected Color lightGreen = new Color(0,255 / 255f,36/255f);
    protected Color lightYellow = new Color(1,255 / 255f,0 /255f);
    protected Color lightRed = new Color(1,36 / 255f,36 /255f);
    protected Color lightDead = new Color(1,0 / 255f,0 /255f);
    private Animator machineAnimator;

    public HealthState currentHealthState;
    private MonoBehaviour damageScript;  // Assign a script in the Inspector


// private void Update()
//     {
//         HealthState previousHealthState = currentHealthState;
//         SetHealthState();

//         if (previousHealthState != currentHealthState)
//         {
//             switch (currentHealthState)
//             {
//                 case HealthState.Damaged:
//                     OnHealthDamaged();
//                     break;
//                 case HealthState.BarelyOperable:
//                     OnHealthBarelyOperable();
//                     break;
//                 case HealthState.FuckedUp:
//                     OnHealthFuckedUp();
//                     break;
//             }
//         }
//     }
//      public virtual void TakeDamage(int damageAmount)
//     {
//         health -= damageAmount;
//         if (health <= 0)
//         {
//             DestroyMachine();
//         }
//         else
//         {
//             if (damageScript != null)
//             {
//                 // Assuming your damage script has a method called OnDamaged
//                 damageScript.Invoke("OnDamaged", 0f);
//             }
//         }
//     }

//     protected virtual void SetHealthState()
//     {
//         if (health > maxHealth * 0.75f)
//         {
//             currentHealthState = HealthState.Healthy;
//             machineAnimator.SetTrigger("Health_green");
//         }
//         else if (health > maxHealth * 0.5f)
//         {
//             currentHealthState = HealthState.Damaged;
//             machineAnimator.SetTrigger("Health_yellow");
//         }
//         else if (health > maxHealth * 0.25f)
//         {
//             currentHealthState = HealthState.BarelyOperable;
//             machineAnimator.SetTrigger("Health_red");
//         }
//         else
//         {
//             currentHealthState = HealthState.FuckedUp;
//             machineAnimator.SetTrigger("Health_dead");
//         }
//     }

    public virtual void OnHealthHealthy()
    {
        // To be implemented in derived classes if needed
    }

    public virtual void OnHealthDamaged()
    {
        // To be implemented in derived classes
    }

    public virtual void OnHealthBarelyOperable()
    {
        // To be implemented in derived classes
    }

    public virtual void OnHealthFuckedUp()
    {
        // To be implemented in derived classes
    }

}