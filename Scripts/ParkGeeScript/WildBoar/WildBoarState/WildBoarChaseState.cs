using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WildBoarChaseState : EnemyState<WildBoarEnum>
{
    private Transform _playerTrm;
    private WildBoar _wildBoar;

    public WildBoarChaseState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _playerTrm = PlayerManager.Instance.PlayerTrm;
        _wildBoar = enemy as WildBoar;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Player player = enemy.IsPlayerDetected();

        if (player != null && enemy.IsPlayerInAttackRange() == null)
        {
            Vector2 dir = player.transform.position - enemy.transform.position;
            if (Mathf.Abs(dir.y) > 2f || enemy.IsObstacleInLine(dir.magnitude))
            {
                enemyStateMachine.ChangeState(WildBoarEnum.Idle);
            }
            else
                Move();
        }
        else
        {
            enemyStateMachine.ChangeState(WildBoarEnum.Idle);
        }
    }

    private void Move()
    {
        Vector2 moveDir = _playerTrm.position - enemy.transform.position;
        moveDir.y = 0;
        moveDir.Normalize();

        int prevFacing = enemy.FacingDir;
        enemy.FlipController(moveDir.x);
        if (enemy.IsGroundDetected(moveDir) && enemy.IsWallDetected() == false)
            enemy.MovementCompo.SetVelocity(moveDir * enemy.Stat.moveSpeed.GetValue());
        else
        {
            enemy.FlipController(prevFacing);
            enemyStateMachine.ChangeState(WildBoarEnum.Idle);
        }
    }
}
