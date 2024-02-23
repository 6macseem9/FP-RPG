using DG.Tweening;
using UnityEngine;

public class WeaponState : State
{
    protected WeaponController _controller;
    protected Animator _animator;
    protected StateMachine _stateMachine;

    public WeaponState(WeaponController controller, Animator animator, StateMachine stateMachine)
    {
        _controller = controller;
        _animator = animator;
        _stateMachine = stateMachine;
    }

    public override void FixedUpdate()
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

    }

    protected void WaitForAnimFinish(TweenCallback func = null)
    {
        if (func == null) func = () => CompleteState();

        Util.Delay(0.05f, () => {
            var animDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
            Util.Delay(animDuration, func);
        });
        
    }
}
