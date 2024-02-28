using DG.Tweening;
using UnityEngine;

public class WeaponState : State
{
    protected WeaponController _controller;
    protected Animator _animator;
    protected StateMachine _stateMachine;

    private Tweener _delay;
    private Tweener _startup;

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
        if (_startup != null) _startup.Kill();
        if (_delay!=null) _delay.Kill();
    }

    public override void Update()
    {

    }

    protected void WaitForAnimFinish()
    {
        _startup = Util.Delay(0.05f, () => DelayComplete());
        
    }

    private void DelayComplete()
    {
        var animDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        _delay = Util.Delay(animDuration, () => CompleteState());
    }
}
