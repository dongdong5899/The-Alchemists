using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdAirBornState : EnemyState<AirBirdEnum>
{
    public AirBirdAirBornState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
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
