using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerState
{
    private float _gravityForce;
    private float _jumpForce;
    private float _jumpTime;
    private Ease _jumpEase1;
    private Ease _jumpEase2;

    private Tweener _tween;
    public bool CanLand;

    public PlayerJump(Player player, float gravityForce,float jumpForce,float jumpTime, Ease jumpEase1, Ease jumpEase2) : base(player)
    {
        _gravityForce = gravityForce;
        _jumpForce = jumpForce;
        _jumpTime = jumpTime;
        _jumpEase1 = jumpEase1;
        _jumpEase2 = jumpEase2;
    }

    public override void FixedUpdate()
    {
        _player.Move();
    }


    public override void OnEnter()
    {
        CanLand = false;

        _player.YVelocity = _jumpForce;
        _tween = DOTween.To(() => _player.YVelocity, x => _player.YVelocity = x, 0, _jumpTime / 2).SetEase(_jumpEase1);
        _tween.onComplete =
            () => {
                CanLand = true;
                _tween = DOTween.To(() => _player.YVelocity, x => _player.YVelocity = x, _gravityForce, _jumpTime / 2).SetEase(_jumpEase2);
                _tween.onComplete = () => CompleteState();
            };

    }

    public override void OnExit()
    {
        _tween.Kill();
    }

    public override void Update()
    {
        
    }
}
