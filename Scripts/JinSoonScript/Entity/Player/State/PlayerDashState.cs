using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) 
    {
        _dashAttackColl = player.transform.Find("DashAttackCollider").GetComponent<Collider2D>();
        _dashTrail = player.transform.Find("DashTrail").GetComponent<ParticleSystem>();
    }

    private Collider2D _dashAttackColl;
    private ParticleSystem _dashTrail;
    private float dashTime;
    private float xInput;

    public override void Enter()
    {
        base.Enter();

        if (player.IsInvincibleWhileDash == true) player.colliderCompo.enabled = false;

        if (player.IsAttackWhileDash == true)
            _dashAttackColl.enabled = true;
        var mainModule = _dashTrail.main;
        mainModule.flipRotation = player.FacingDir == 1 ? 0 : 1;
        _dashTrail.Play();

        xInput = player.PlayerInput.XInput;

        if (xInput == 0) xInput = player.FacingDir * -0.5f;
        
        dashTime = Time.time;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (player.IsWallDetected() || Time.time - dashTime > player.DashTime)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }

        player.MovementCompo.SetVelocity(new Vector2(player.DashPower * player.FacingDir, 0), true);
    }

    public override void Exit()
    {
        base.Exit();
        if (player.IsInvincibleWhileDash == true) player.colliderCompo.enabled = true;

        if (player.IsAttackWhileDash == true)
            _dashAttackColl.enabled = false;
        _dashTrail.Stop();

        player.MovementCompo.StopImmediately(false);
    }

}
