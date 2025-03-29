using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected int _animBoolHash;
    protected readonly int _yVelocityHash = Animator.StringToHash("y_velocity");
    protected int _animationTriggerBit = 0;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    //���¿� �������� �� ������ �Լ�
    public virtual void Enter()
    {
        player.animatorCompo.SetBool(_animBoolHash, true);
        _animationTriggerBit = 0;
    }

    //���¸� ������ ������ �Լ�
    public virtual void Exit()
    {
        player.animatorCompo.SetBool(_animBoolHash, false);
    }

    //�� ������ �� ����� �Լ�
    public virtual void UpdateState()
    {
        
    }


    public virtual void AnimationTrigger(AnimationTriggerEnum trigger)
    {
        _animationTriggerBit |= (int)trigger;
    }

    protected bool IsTriggerCalled(AnimationTriggerEnum trigger)
    {
        bool isTriggered = (_animationTriggerBit & (int)trigger) != 0;
        if (isTriggered)
            _animationTriggerBit &= ~(int)trigger;
        return isTriggered;
    }
}
