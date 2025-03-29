using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WildBoarIdleState : EnemyState<WildBoarEnum>
{
    public WildBoarIdleState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName)
        : base(enemy, enemyStateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        enemy.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Player player = enemy.IsPlayerDetected();



        if (player != null)
        {
            Vector2 dir = player.transform.position - enemy.transform.position;
            if (Mathf.Abs(dir.y) > 1f || enemy.IsObstacleInLine(dir.magnitude)) return;

            enemy.FlipController(dir.x);
            if (enemy.IsPlayerInAttackRange())
            {
                if (enemy.lastAttackTime + enemy.attackCool < Time.time)
                {
                    enemy.lastAttackTime = Time.time;
                    enemyStateMachine.ChangeState(WildBoarEnum.Rush);
                }
            }
            else
            {
                enemyStateMachine.ChangeState(WildBoarEnum.Chase);
                return;
            }
        }


        //if (player != null && enemy.IsObstacleInLine(enemy.runAwayDistance) == false)
        //    enemyStateMachine.ChangeState(WildBoarEnum.Chase);
    }
}
