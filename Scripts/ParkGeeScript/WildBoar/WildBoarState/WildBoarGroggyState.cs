using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarGroggyState : EnemyState<WildBoarEnum>
{
    public WildBoarGroggyState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName) 
        : base(enemy, enemyStateMachine, animBoolName)
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
    }
}
