using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdPatrolState : EnemyState<AirBirdEnum>
{

    public AirBirdPatrolState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }


    public override void UpdateState()
    {
        base.UpdateState();

        
    }
}
