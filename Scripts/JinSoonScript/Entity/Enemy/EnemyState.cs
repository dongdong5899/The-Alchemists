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

    //상태에 진입했을 때 실행할 함수
    public virtual void Enter()
    {
        enemy.animatorCompo.SetBool(animBoolHash, true);
        triggerCall = false; //애니메이션이 다 끝났을때 실행될 불리언 값
    }

    //상태를 나갈때 실행할 함수
    public virtual void Exit()
    {
        enemy.animatorCompo.SetBool(animBoolHash, false);
    }

    //이 상태일 때 실행될 함수
    public virtual void UpdateState()
    {
        //_player.animatorCompo.SetFloat(_yVelocityHash, _rigidbody.velocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCall = true;
    }
}
