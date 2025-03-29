using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void UpdateState()
    {
        base.UpdateState();
        float xInput = player.PlayerInput.XInput;
        player.MovementCompo.SetVelocity(new Vector2(xInput * player.MoveSpeed, player.MovementCompo.RigidbodyCompo.velocity.y));

        if (Mathf.Abs(xInput) < 0.05f || player.IsWallDetected())
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
