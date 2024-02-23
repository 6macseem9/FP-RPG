using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAttack : WeaponState
{
    public WeaponAttack(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {

    }

    public override void OnEnter()
    {
        _animator.Play(_controller.AttackString);

        WaitForAnimFinish();
    }
    public override void OnExit()
    {
        _controller.Weapon.EnableCollider(false);
    }
}
