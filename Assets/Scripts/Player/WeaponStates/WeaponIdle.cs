using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdle : WeaponState
{
    public WeaponIdle(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void OnEnter()
    {
        _controller.ResetAttackNumber();
        _animator.Play(_controller.WeaponName);
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

    }
}
