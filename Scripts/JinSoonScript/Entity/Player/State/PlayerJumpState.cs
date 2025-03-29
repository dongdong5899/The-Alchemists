using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName){}

    public override void Enter()
    {
        base.Enter();

        float xInput = player.PlayerInput.XInput;

        player.MovementCompo.RigidbodyCompo.velocity = new Vector2(xInput, player.JumpForce);
    }


    public override void UpdateState()
    {
        base.UpdateState();

        //float _dashDir = _player.PlayerInput.XInput;
        //_player.MovementCompo.SetVelocity(_dashDir * _player.MoveSpeed, _rigidbody.velocity.y);

        if (player.MovementCompo.RigidbodyCompo.velocity.y <= 0)
            player.StateMachine.ChangeState(PlayerStateEnum.Fall);
    }
}
