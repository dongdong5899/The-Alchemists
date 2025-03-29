using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatrolState : EnemyState<SlimeStateEnum>
{
    private float _startTime;
    private float _idleCool;

    private Vector2 _moveDir;

    public SlimePatrolState(Enemy<SlimeStateEnum> enemy, EnemyStateMachine<SlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _startTime = Time.time;
        _idleCool = Random.Range(1f, 2f);

        _moveDir = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;

        int prevFacing = enemy.FacingDir;
        enemy.FlipController(_moveDir.x);
        if (enemy.IsFrontGround() == false || enemy.IsWallDetected())
        {
            _moveDir *= -1;
            enemy.Flip();
            if (enemy.IsFrontGround() == false || enemy.IsWallDetected())
            {
                enemy.FlipController(prevFacing);
                enemyStateMachine.ChangeState(SlimeStateEnum.Idle);
            }
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        enemy.MovementCompo.SetVelocity(_moveDir * enemy.EnemyStat.moveSpeed.GetValue());

        if (enemy.IsPlayerDetected())
        {
            enemyStateMachine.ChangeState(SlimeStateEnum.Chase);
        }

        if (_startTime + _idleCool < Time.time)
        {
            enemyStateMachine.ChangeState(SlimeStateEnum.Idle);
        }
    }
}
