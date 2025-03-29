using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatIdleState : EnemyState<GoatEnum>
{
    public GoatIdleState(Enemy<GoatEnum> enemy, EnemyStateMachine<GoatEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    private float _startTime;
    private float _patrolCool;


    public override void Enter()
    {
        base.Enter();

        enemy.MovementCompo.StopImmediately();


        _startTime = Time.time;
        _patrolCool = Random.Range(1f, 3f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Player player;
        if (player = enemy.IsPlayerInAttackRange())
        {
            if (enemy.IsWallDetected())
                enemy.FlipController(player.transform.position.x - enemy.transform.position.x);
            else if (enemy.lastAttackTime + enemy.attackCool < Time.time)
            {
                enemy.lastAttackTime = Time.time;
                enemyStateMachine.ChangeState(GoatEnum.Attack);
            }
            else if (enemy.IsPlayerInAttackRange(2) == null)
            {
                int prevFacing = enemy.FacingDir;
                enemy.FlipController(enemy.transform.position.x - player.transform.position.x);
                if (enemy.IsFrontGround())
                {
                    enemy.FlipController(prevFacing);
                    enemyStateMachine.ChangeState(GoatEnum.Chase);
                }
                else
                    enemy.FlipController(prevFacing);
            }
        }
        else if (player = enemy.IsPlayerDetected())
        {
            if (enemy.IsWallDetected())
                enemy.FlipController(player.transform.position.x - enemy.transform.position.x);
            else
                enemyStateMachine.ChangeState(GoatEnum.Chase);
        }
        else if (_startTime + _patrolCool < Time.time)
        {
            enemyStateMachine.ChangeState(GoatEnum.Patrol);
        }
    }
}
