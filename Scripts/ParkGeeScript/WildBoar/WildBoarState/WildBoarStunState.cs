using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarStunState : EnemyState<WildBoarEnum>
{
    public WildBoarStunState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.CanStateChangeable = false;
        enemy.OnStunSprite(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animatorCompo.speed = 1;
        enemy.OnStunSprite(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (enemy.IsGroundDetected() && enemy.rigidbodyCompo.velocity.x != 0)
        {
            enemy.MovementCompo.StopImmediately();
        }

        if (Time.time > enemy.stunEndTime && enemy.IsUnderStatusEffect(StatusDebuffEffectEnum.Floating) == false)
        {
            enemy.CanStateChangeable = true;
            enemyStateMachine.ChangeState(WildBoarEnum.Idle);
        }
    }
}
