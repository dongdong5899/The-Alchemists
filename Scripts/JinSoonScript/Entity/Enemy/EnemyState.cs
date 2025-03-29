using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<T> where T : Enum
{
    protected EnemyStateMachine<T> enemyStateMachine;
    protected Enemy<T> enemy;
    protected Rigidbody2D rigidbody;

    protected int animBoolHash;
    protected bool triggerCall;

    public EnemyState(Enemy<T> enemy, EnemyStateMachine<T> enemyStateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);
        rigidbody = this.enemy.rigidbodyCompo;
    }

    //���¿� �������� �� ������ �Լ�
    public virtual void Enter()
    {
        enemy.animatorCompo.SetBool(animBoolHash, true);
        triggerCall = false; //�ִϸ��̼��� �� �������� ����� �Ҹ��� ��
    }

    //���¸� ������ ������ �Լ�
    public virtual void Exit()
    {
        enemy.animatorCompo.SetBool(animBoolHash, false);
    }

    //�� ������ �� ����� �Լ�
    public virtual void UpdateState()
    {
        //_player.animatorCompo.SetFloat(_yVelocityHash, _rigidbody.velocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCall = true;
    }
}
