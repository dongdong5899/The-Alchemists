using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.MovementCompo.StopImmediately(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float xInput = player.PlayerInput.XInput;

        if(!player.IsGroundDetected())
            stateMachine.ChangeState(PlayerStateEnum.Fall);

        if (player.canClimb && player.PlayerInput.YInput != 0)
            stateMachine.ChangeState(PlayerStateEnum.Climb);

        //이동방향과 벽방향이 같다면 return
        if (Mathf.Abs(player.FacingDir + xInput) > 1.5f && player.IsWallDetected())
            return;

        if (Mathf.Abs(xInput) > 0.05f)
            stateMachine.ChangeState(PlayerStateEnum.Move);
    }
}
