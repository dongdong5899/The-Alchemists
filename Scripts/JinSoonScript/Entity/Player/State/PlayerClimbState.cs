using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimbState : PlayerState
{
    private Skill dashSkill;
    private Skill normalAttackSkill;
    private float _lastClimbTime = 0;
    private float _climbDelay = 0.3f;

    private readonly int _inputHash = Animator.StringToHash("Input");

    public PlayerClimbState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        dashSkill = player.SkillSO.GetSkillByEnum(PlayerSkillEnum.Dash).skill;
        normalAttackSkill = player.SkillSO.GetSkillByEnum(PlayerSkillEnum.NormalAttack).skill;
    }

    public override void Enter()
    {
        base.Enter();
        if (_lastClimbTime + _climbDelay > Time.time)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
        player.PlayerInput.JumpEvent += HandleJumpEvent;
        player.PlayerInput.AttackEvent += HandleAttackEvent;
        player.PlayerInput.OnXInputEvent += HandleXInputEvent;
        player.MovementCompo.StopImmediately(true);
        Vector3 offset = player.CurrentVine.transform.position - player.transform.position;

        player.transform.position += new Vector3(offset.x, 0.8f);
        player.SetGravityActive(false);
    }

    public override void Exit()
    {
        player.PlayerInput.JumpEvent -= HandleJumpEvent;
        player.PlayerInput.AttackEvent -= HandleAttackEvent;
        player.PlayerInput.OnXInputEvent -= HandleXInputEvent;
        _lastClimbTime = Time.time;

        player.SetGravityActive(true);
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float xInput = player.PlayerInput.XInput;
        float yInput = player.PlayerInput.YInput;

        player.animatorCompo.SetInteger(_inputHash, Mathf.CeilToInt(yInput));
        player.MovementCompo.SetVelocity(new Vector2(0, yInput * player.MoveSpeed), false, true);
        if (player.CurrentVine.CurrentState != VineState.Grown || player.IsGroundDetected(new Vector3(0, -0.5f, 0)))
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    #region HandleEventSection

    private void HandleJumpEvent()
    {
        stateMachine.ChangeState(PlayerStateEnum.Jump);
        player.CanJump = false;
    }

    private void HandleXInputEvent(float input)
    {
        if (input > 0.5f)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    private void HandleAttackEvent()
    {
        normalAttackSkill.UseSkill();
    }

    #endregion
}
