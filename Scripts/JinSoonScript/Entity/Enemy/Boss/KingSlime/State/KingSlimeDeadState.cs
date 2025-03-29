using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeDeadState : EnemyState<KingSlimeStateEnum>
{
    public KingSlimeDeadState(Enemy<KingSlimeStateEnum> enemy, EnemyStateMachine<KingSlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
}
