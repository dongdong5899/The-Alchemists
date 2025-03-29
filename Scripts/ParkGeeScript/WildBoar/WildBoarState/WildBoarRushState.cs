using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarRushState : EnemyState<WildBoarEnum>
{
    private WildBoar _wildBoar;
    private bool _isRushing = false;
    private Vector2 _rushDir;
    private float _rushSpeed = 20f;

    public WildBoarRushState(Enemy<WildBoarEnum> enemy, EnemyStateMachine<WildBoarEnum> enemyStateMachine, string animBoolName)
        : base(enemy, enemyStateMachine, animBoolName)
    {
        _wildBoar = enemy as WildBoar;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        if (!_isRushing)
        {
            //대쉬 시작
            _isRushing = true;
            _wildBoar.dashAttackCollider.SetActive(true);
        }
        else
        {
            //대쉬 끝
            enemy.CanStateChangeable = true;
            enemyStateMachine.ChangeState(WildBoarEnum.Idle);
        }
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.Instance.PlaySound(SoundEnum.PigRush, enemy.transform);

        _isRushing = false;
        _rushDir = Vector2.right * enemy.FacingDir;
        enemy.MovementCompo.StopImmediately(false);
        enemy.CanStateChangeable = false;
    }

    public override void Exit()
    {
        _wildBoar.dashAttackCollider.SetActive(false);
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if (enemy.IsFrontGround() == false)
        {
            enemy.CanStateChangeable = true;
            enemyStateMachine.ChangeState(WildBoarEnum.Idle);
            return;
        }

        if (enemy.IsWallDetected() == true)
        {
            Vector2 dir = new Vector2(-enemy.FacingDir * 5, 10);
            enemy.KnockBack(dir);
            enemy.Stun(2);
        }

        if (_isRushing)
            enemy.MovementCompo.SetVelocity(_rushDir * _rushSpeed);
    }
}
