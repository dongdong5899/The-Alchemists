using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.CanStateChangeable = false;
        player.OnStunSprite(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Time.time > player.stunEndTime)
        {
            player.CanStateChangeable = true;
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.OnStunSprite(false);
    }
}
