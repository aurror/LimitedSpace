using UnityEngine;

public class MachineAnimatorBehaviour : StateMachineBehaviour
{


    const int healthHash = -1821893101;
    const int healthHash2 = 412110831;
    const int healthHash3 = -2013558961;
    const int healthHash4 = 1983648076;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MachineManager machine = animator.transform.parent.GetComponent<MachineManager>();
        if (machine != null)
        {
            Animator.StringToHash("Health_green");
            switch (stateInfo.shortNameHash)
            {
                case healthHash:
                    machine.OnHealthHealthy();
                    break;
                case healthHash2:
                    machine.OnHealthDamaged();
                    break;
                case healthHash3:
                    machine.OnHealthBarelyOperable();
                    break;
                case healthHash4:
                    machine.OnHealthFuckedUp();
                    break;
            }
        }
    }


}