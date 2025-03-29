using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeIdleState : EnemyState<KingSlimeStateEnum>
{
    private KingSlime _kingSlime;
    private float _idleEndTime;

    public KingSlimeIdleState(Enemy<KingSlimeStateEnum> enemy, EnemyStateMachine<KingSlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _kingSlime = enemy as KingSlime;
    }

    public override void Enter()
    {
        base.Enter();
        _kingSlime.SetCanBeStun(true);
        _idleEndTime = Time.time + _kingSlime.idleTime;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Time.time > _idleEndTime)
        {
            enemyStateMachine.ChangeState(GetRandomPatternState());
        }
    }

    public override void Exit()
    {
        _kingSlime.SetCanBeStun(false);
        base.Exit();
    }

    public KingSlimeStateEnum GetRandomPatternState()
    {
        int rand = Random.Range(1, 3);
        return (KingSlimeStateEnum)rand;
    }
}
