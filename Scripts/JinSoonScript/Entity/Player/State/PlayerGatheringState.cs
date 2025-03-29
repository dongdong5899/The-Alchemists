using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatheringState : PlayerState
{
    public PlayerGatheringState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.MovementCompo.StopImmediately(true);
    }
}
