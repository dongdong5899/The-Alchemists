using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState 
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {
            DiePanel diePanel = UIManager.Instance.GetUI(UIType.PlayerDie) as DiePanel;
            diePanel.Init((int)(Time.time - GameManager.Instance.playStartTime), GameManager.Instance.killCnt, GameManager.Instance.gatherCnt, GameManager.Instance.GetPlayerProgress());
            diePanel.Open();
        }
    }
}
