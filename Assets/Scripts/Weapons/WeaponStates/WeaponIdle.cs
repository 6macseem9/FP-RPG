using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdle : WeaponState
{
    public WeaponIdle(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void OnEnter()
    {
        _controller.ResetAttackNumber();
        if(_stateMachine.PreviousStateName!=nameof(WeaponBlock))_animator.Play("idle");
    }

}
