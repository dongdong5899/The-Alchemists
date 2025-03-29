using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GoatAttackState : EnemyState<GoatEnum>
{
    public GoatAttackState(Enemy<GoatEnum> enemy, EnemyStateMachine<GoatEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    private float _damageRadius = 2.5f;
    private Player _player;
    private bool _isFall = false;

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        //���� Idle�� ��ȯ�ϴϱ� ��ڿ� ������ Patrol�� ��
        //enemyStateMachine.ChangeState(SlimeStateEnum.Idle);
    }

    public override void Enter()
    {
        base.Enter();

        _isFall = false;

        _player = enemy.IsPlayerInAttackRange();
        Vector2 dir = _player.transform.position - enemy.transform.position;
        dir.y = enemy.Stat.jumpForce.GetValue();
        dir.x *= (enemy.MovementCompo.RigidbodyCompo.gravityScale * -Physics2D.gravity.y) / (2 * dir.y);

        enemy.MovementCompo.SetVelocity(dir, withYVelocity: true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (enemy.MovementCompo.RigidbodyCompo.velocity.y < 0 && enemy.IsGroundDetected())
        {
            //���� ��ü �ʿ�
            AudioManager.Instance.PlaySound(SoundEnum.SlimeAttack, enemy.transform.position);

            Vector2 dir = _player.transform.position - enemy.transform.position;
            if (dir.magnitude < _damageRadius)
            {
                dir.y = 5;
                dir.Normalize();
                if (_player.healthCompo.TakeDamage((int)enemy.Stat.globalDamageInflict.GetValue(), dir * 4f, enemy) == false)
                {
                    dir.x *= -1;
                    enemy.healthCompo.TakeDamage(_player.parryingLevel * 5, dir * 4f, _player);
                    if (_player.parryingLevel == 2)
                    {
                        enemy.Stun(1);
                    }
                }
            }

            enemyStateMachine.ChangeState(GoatEnum.Idle);
        }
    }
}
