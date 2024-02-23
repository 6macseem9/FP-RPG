using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecovery : WeaponState
{
    private bool _interrupted;

    public WeaponRecovery(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void OnEnter()
    {
        _interrupted = false;
        _animator.Play(_controller.AttackString + "r");

        WaitForAnimFinish(() => { if (!_interrupted) CompleteState(); });
    }

    public override void OnExit()
    {
        _interrupted = true;
    }

    public override void Update()
    {

    }
}
