using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAirState : PlayerState
{
    private BoxCollider2D _playerCollider;
    private Collider2D[] _colliders;
    private LayerMask _whatIsVine = LayerMask.GetMask("Vine");
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) 
    {
        _playerCollider = player.colliderCompo as BoxCollider2D;
        _colliders = new Collider2D[1];
    }

    public override void Enter()
    {
        base.Enter();
        if (player.curJumpCnt < 1)
            player.curJumpCnt = 1;
        player.PlayerInput.JumpEvent += HandleJumpEvent;
        player.PlayerInput.DashEvent += HandleDashEvent;
        player.PlayerInput.AttackEvent += HandleAttackEvent;
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerInput.JumpEvent -= HandleJumpEvent;
        player.PlayerInput.DashEvent -= HandleDashEvent;
        player.PlayerInput.AttackEvent -= HandleAttackEvent;
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (player.PlayerInput.YInput > 0.5f)
        {
            int count = Physics2D.OverlapBoxNonAlloc(player.PlayerCenter.position, _playerCollider.size, 0, _colliders, _whatIsVine);
            if (count != 0)
            {
                Collider2D collider = _colliders[0];
                player.CurrentVine = collider.transform.GetComponent<GrowingGrass>();
                if (player.CurrentVine.CurrentState == VineState.Grown)
                    stateMachine.ChangeState(PlayerStateEnum.Climb);
                return;
            }
        }

        //떨어질 때는 조금 천천히 움직여지게
        float xInput = player.PlayerInput.XInput;

        if (Mathf.Abs(player.FacingDir + xInput) > 1.5f && player.IsWallDetected()) return;

        player.MovementCompo.SetVelocity(new Vector2(player.MoveSpeed * xInput, player.MovementCompo.RigidbodyCompo.velocity.y));
    }

    private void HandleJumpEvent()
    {
        if (player.CanJump)
        {
            player.curJumpCnt++;
            stateMachine.ChangeState(PlayerStateEnum.Jump);
            //_player.CanJump = false;
        }
    }

    private void HandleDashEvent() => player.SkillSO.GetSkillByEnum(PlayerSkillEnum.Dash).skill.UseSkill();
    private void HandleAttackEvent() => player.SkillSO.GetSkillByEnum(PlayerSkillEnum.NormalAttack).skill.UseSkill();
}
