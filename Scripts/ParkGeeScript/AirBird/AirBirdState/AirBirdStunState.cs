using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdStunState : EnemyState<AirBirdEnum>
{
    public AirBirdStunState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.CanStateChangeable = false;
        enemy.rigidbodyCompo.gravityScale = 3.5f;
        enemy.OnStunSprite(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animatorCompo.speed = 1;
        enemy.OnStunSprite(true);
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
            enemyStateMachine.ChangeState(AirBirdEnum.Idle);
        }
    }
}
