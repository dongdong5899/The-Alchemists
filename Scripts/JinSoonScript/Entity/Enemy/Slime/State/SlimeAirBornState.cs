using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAirBornState : EnemyState<SlimeStateEnum>
{
    public SlimeAirBornState(Enemy<SlimeStateEnum> enemy, EnemyStateMachine<SlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
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
}
