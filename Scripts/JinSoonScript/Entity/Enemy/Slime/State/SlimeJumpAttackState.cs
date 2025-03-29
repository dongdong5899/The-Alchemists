using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumpAttackState : EnemyState<SlimeStateEnum>
{
    private float _damageRadius = 2.5f;
    private Player _player;
    private bool _isFall = false;

    private int _jumpDownAnimHash = Animator.StringToHash("JumpDown");
    private int _landAnimHash = Animator.StringToHash("Land");

    public SlimeJumpAttackState(Enemy<SlimeStateEnum> enemy, EnemyStateMachine<SlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        //망할 Idle로 전환하니까 등뒤에 있으면 Patrol로 감
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

        if (enemy.MovementCompo.RigidbodyCompo.velocity.y < 0)
        {
            if (_isFall == false)
            {
                enemy.animatorCompo.SetTrigger(_jumpDownAnimHash);
                _isFall = true;
            }

            if (enemy.IsGroundDetected())
            {
                AudioManager.Instance.PlaySound(SoundEnum.SlimeAttack, enemy.transform.position);

                enemy.animatorCompo.SetTrigger(_landAnimHash);

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

                enemyStateMachine.ChangeState(SlimeStateEnum.Idle);
            }
        }
    }
}
