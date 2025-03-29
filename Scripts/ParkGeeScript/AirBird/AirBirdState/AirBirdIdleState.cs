using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdIdleState : EnemyState<AirBirdEnum>
{
    public AirBirdIdleState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.MovementCompo.RigidbodyCompo.gravityScale = 0;
        enemy.MovementCompo.StopImmediately(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Player player;
        if(player = enemy.IsPlayerDetected())
        {
            Vector2 playerDir = player.transform.position + Vector3.up - enemy.transform.position;
            if (enemy.IsObstacleInLine(playerDir.magnitude)) return;

            enemy.FlipController(playerDir.x);

            if (enemy.IsPlayerInAttackRange())
            {
                if (enemy.lastAttackTime + enemy.attackCool < Time.time)
                {
                    enemy.lastAttackTime = Time.time;
                    enemyStateMachine.ChangeState(AirBirdEnum.Shoot);
                }
            }
            else
            {
                enemyStateMachine.ChangeState(AirBirdEnum.Chase);
            }
        }

        if (enemy.IsGroundDetected(distance: 4f))
        {
            enemy.MovementCompo.SetVelocity(Vector2.up * enemy.Stat.moveSpeed.GetValue(), withYVelocity: true);
        }
        else if (enemy.rigidbodyCompo.velocity.y != 0)
        {
            enemy.MovementCompo.SetVelocity(Vector2.zero, withYVelocity: true);
        }
    }
}
