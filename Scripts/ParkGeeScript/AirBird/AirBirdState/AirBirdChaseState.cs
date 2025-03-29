using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdChaseState : EnemyState<AirBirdEnum>
{
    private AirBird _airBird;

    public AirBirdChaseState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _airBird = enemy as AirBird;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Player player = enemy.IsPlayerDetected();

        if (player == null)
        {
            enemyStateMachine.ChangeState(AirBirdEnum.Idle);
            return;
        }

        Vector2 dir = player.transform.position + Vector3.up - enemy.transform.position;
        if (enemy.IsObstacleInLine(dir.magnitude))
        {
            enemyStateMachine.ChangeState(AirBirdEnum.Idle);
            return;
        }

        if (enemy.IsPlayerInAttackRange())
        {

            if (enemy.lastAttackTime + enemy.attackCool < Time.time)
            {
                enemy.lastAttackTime = Time.time;
                enemyStateMachine.ChangeState(AirBirdEnum.Shoot);
            }
            else
                enemyStateMachine.ChangeState(AirBirdEnum.Idle);
        }
        else
        {
            bool withYVelocity = true;
            if (enemy.IsGroundDetected(distance: 4f))
            {
                dir = (dir.normalized + Vector2.up * 1.5f);
            }
            else if (enemy.IsGroundDetected(distance: 5f))
            {
                withYVelocity = dir.y > 0;
            }
            enemy.MovementCompo.SetVelocity(dir.normalized * enemy.Stat.moveSpeed.GetValue(), withYVelocity: withYVelocity);
        }
    }
}
