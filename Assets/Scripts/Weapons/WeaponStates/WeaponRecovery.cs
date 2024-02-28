using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponRecovery : WeaponState
{


    public WeaponRecovery(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }


    public override void OnEnter()
    {
        _animator.Play(_controller.AttackNumber + "recovery");

        WaitForAnimFinish();
    }

}
