using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerState
{
    private float _gravityForce;
    private float _coyoteTimeDuration;
    private StateMachine _stateMachine;

    private float _actualGravity;
    private Tweener _tween;
    public bool CoyoteTime;

    public PlayerFall(Player player, float gravityForce, float coyoteTime, StateMachine stateMachine) : base(player)
    {
        _gravityForce = gravityForce;
        _coyoteTimeDuration = coyoteTime;
        _stateMachine = stateMachine;
    }

    public override void FixedUpdate()
    {
        _player.YVelocity = _actualGravity;
        _player.Move();
    }

    public override void OnEnter()
    {
        _actualGravity = _gravityForce;
        _tween = DOTween.To(() => _actualGravity, x => _actualGravity = x, _gravityForce*10f, 6).SetEase(Ease.InSine);

        CoyoteTime = _stateMachine.PreviousStateName == nameof(PlayerGrounded);
        Util.Delay(_coyoteTimeDuration, () =>  CoyoteTime = false);
    }

    public override void OnExit()
    {
        _tween.Kill();
    }

    public override void Update()
    {

    }
}
