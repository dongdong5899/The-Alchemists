using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPushState : PlayerState
{
    private readonly int _inputHash = Animator.StringToHash("Input");

    private Transform _pushObjectPosTrm;
    private Rigidbody2D _pushObjectRigid;
    public PlayerPushState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        _pushObjectPosTrm = player.transform.Find("Visual/PushObjectPos");
    }

    public override void Enter()
    {
        base.Enter();
        _pushObjectRigid = player.CurrentPushTrm.GetComponent<Rigidbody2D>();
        _pushObjectRigid.velocity = Vector3.zero;
        Vector3 offset = new Vector3((player.CurrentPushTrm.GetComponent<BoxCollider2D>().size.x / 2) * player.CurrentPushTrm.localScale.x, 0);
        float prevY = player.CurrentPushTrm.position.y;
        player.CurrentPushTrm.position = _pushObjectPosTrm.position + offset * player.FacingDir;
        player.CurrentPushTrm.position = new Vector3(player.CurrentPushTrm.position.x, prevY);

        player.PlayerInput.InteractPress += HandleInteract;
        player.PlayerInput.JumpEvent += HandleJump;
    }

    private void HandleJump()
    {
        stateMachine.ChangeState(PlayerStateEnum.Jump);
    }

    public override void Exit()
    {
        _pushObjectRigid.velocity = new Vector2(0, _pushObjectRigid.velocity.y);
        player.CurrentPushTrm = null;

        player.PlayerInput.InteractPress -= HandleInteract;
        player.PlayerInput.JumpEvent -= HandleJump;
        base.Exit();
    }

    private void HandleInteract()
    {
        stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void UpdateState()
    {
        if(Mathf.Abs(player.transform.position.y + 0.5f - player.CurrentPushTrm.position.y) > 1f)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
        float xInput = player.PlayerInput.XInput;
        _pushObjectRigid.velocity = new Vector2(xInput * 4, _pushObjectRigid.velocity.y);
        player.MovementCompo.SetVelocity(new Vector2(xInput * 4, player.MovementCompo.RigidbodyCompo.velocity.y), true);
        player.animatorCompo.SetInteger(_inputHash, Mathf.CeilToInt(xInput));
    }
}
