using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeStunState : EnemyState<KingSlimeStateEnum>
{
    public KingSlimeStunState(Enemy<KingSlimeStateEnum> enemy, EnemyStateMachine<KingSlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.OnStunSprite(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Time.time > enemy.stunEndTime)
        {
            enemyStateMachine.ChangeState(KingSlimeStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        enemy.OnStunSprite(false);
        base.Exit();
    }
}
