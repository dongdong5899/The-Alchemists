using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    private Skill dashSkill;
    private Skill normalAttackSkill;
    private BoxCollider2D _playerCollider;

    private LayerMask _whatIsVine = LayerMask.GetMask("Vine");

    private Collider2D[] _colliders;

    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        dashSkill = player.SkillSO.GetSkillByEnum(PlayerSkillEnum.Dash).skill;
        normalAttackSkill = player.SkillSO.GetSkillByEnum(PlayerSkillEnum.NormalAttack).skill;
        _playerCollider = player.colliderCompo as BoxCollider2D;
        _colliders = new Collider2D[1];
    }

    public override void Enter()
    {
        base.Enter();
        player.curJumpCnt = 0;
        player.PlayerInput.JumpEvent += HandleJumpEvent;
        player.PlayerInput.DashEvent += HandleDashEvent;
        player.PlayerInput.AttackEvent += HandleAttackEvent;
        player.PlayerInput.OnTryUseQuickSlot += HandleTryUseQuickSlotEvent;
        player.PlayerInput.InteractPress += HandleInteractEvent;
        player.PlayerInput.OnYInputEvent += HandleYInputEvent;
    }

    

    private void HandleInteractEvent()
    {
        Transform trm = player.CheckObjectInFront();
        if (trm != null)
        {
            player.CurrentPushTrm = trm;
            stateMachine.ChangeState(PlayerStateEnum.Push);
        }
    }


    public override void Exit()
    {
        player.PlayerInput.JumpEvent -= HandleJumpEvent;
        player.PlayerInput.DashEvent -= HandleDashEvent;
        player.PlayerInput.AttackEvent -= HandleAttackEvent;
        player.PlayerInput.OnTryUseQuickSlot -= HandleTryUseQuickSlotEvent;
        player.PlayerInput.InteractPress -= HandleInteractEvent;
        player.PlayerInput.OnYInputEvent -= HandleYInputEvent;
        base.Exit();
    }


    public override void UpdateState()
    {
        base.UpdateState();

       

        if (player.IsGroundDetected() == false)
            player.StartDelayCallBack(player.CoyoteTime, () => player.CanJump = false);
        else
            player.CanJump = true;

        if (player.CanJump == false && !player.IsGroundDetected())
        {
            player.CanJump = false;
            stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    #region HandleEventSection

    private void HandleJumpEvent()
    {
        if (player.PlayerInput.YInput < 0)
        {
            player.CheckOneWayPlatform();
            return;
        }

        if (player.CanJump)
        {
            player.curJumpCnt = 1;
            stateMachine.ChangeState(PlayerStateEnum.Jump);
            //_player.CanJump = false;
        }
    }

    private void HandleDashEvent()
    {
        dashSkill.UseSkill();
    }

    private void HandleAttackEvent()
    {
        normalAttackSkill.UseSkill();
    }

    private void HandleTryUseQuickSlotEvent()
    {
        QuickSlot slot = QuickSlotManager.Instance.GetSelectedPotionSlot();
        if (slot == null || slot.assignedItem == null) return;

        if (slot.assignedItem.itemSO is ThrowPotionItemSO)
        {
            stateMachine.ChangeState(PlayerStateEnum.Throw);
        }
        else
        {
            DrinkPotion drinkPotion = new DrinkPotion();
            drinkPotion.Init(slot, player);
            drinkPotion.UsePotion();
        }
    }

    private void HandleYInputEvent(float input)
    {
        if (input <= 0) return;
        int count = Physics2D.OverlapBoxNonAlloc(player.PlayerCenter.position, _playerCollider.size, 0, _colliders, _whatIsVine);

        if (count != 0)
        {
            Collider2D collider = _colliders[0];
            player.CurrentVine = collider.transform.GetComponent<GrowingGrass>();
            if (player.CurrentVine.CurrentState == VineState.Grown)
                stateMachine.ChangeState(PlayerStateEnum.Climb);
        }
    }

    #endregion

}
