using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : PlayerState
{
    public PlayerGrounded(Player player) : base(player)
    {
    
    }

    public override void FixedUpdate()
    {
        _player.YVelocity = 0;
        _player.Move();
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
}
