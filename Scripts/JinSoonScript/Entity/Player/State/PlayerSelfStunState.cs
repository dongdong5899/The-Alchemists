using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelfStunState : PlayerState
{
    public PlayerSelfStunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("셀프스턴스테이트");
        player.CanStateChangeable = false;
        player.StartDelayCallBack(player.stunEndTime, () =>
        {
            player.CanStateChangeable = true;
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        });

    }
}
