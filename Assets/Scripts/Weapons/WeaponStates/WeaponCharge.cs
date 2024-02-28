using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharge : WeaponState
{
    private Tweener _tween;

    public WeaponCharge(WeaponController controller, Animator animator, StateMachine stateMachine) : base(controller, animator, stateMachine)
    {
    }

    public override void OnEnter()
    {
        _animator.Play(_controller.AttackNumber + "charge");
        _controller.ÑhargeModifier = _controller.Weapon.StartCharge;

        Util.Delay(0.05f, () => {
            var animDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
            _tween = DOTween.To(() => _controller.ÑhargeModifier, x => _controller.ÑhargeModifier = x, 1, animDuration).SetEase(Ease.Linear);
        });
        
    }

    public override void OnExit()
    {
        _tween.Kill();
    }
}
