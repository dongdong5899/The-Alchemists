using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatAirBornState : EnemyState<GoatEnum>
{
    public GoatAirBornState(Enemy<GoatEnum> enemy, EnemyStateMachine<GoatEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }
}
