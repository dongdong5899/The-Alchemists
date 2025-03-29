using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarPatrolState : EnemyState<WildBoarEnum>
{
    public WildBoarPatrolState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }
}
