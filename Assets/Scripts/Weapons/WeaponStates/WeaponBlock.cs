using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlock : WeaponState
{
    private bool _leftIdle;

    public WeaponBlock(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void OnEnter()
    {
        _leftIdle = false;
        Util.Delay(0.1f, () => _leftIdle = true);

        _animator.Play("block");
        
    }

    public override void OnExit()
    {
        base.OnExit();

        _animator.Unpause();
    }

    public override void Update()
    {
        if (_animator.GetFloat("Speed")==0 || (_leftIdle && _animator.GetCurrentAnimatorStateInfo(0).IsName("idle")))
        {
            CompleteState();
        }
        
    }
}
