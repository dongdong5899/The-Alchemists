using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum WildBoarEnum
{
    Idle,
    Patrol,
    Chase,
    Rush,
    Groggy,
    Stun,
    AirBorn,
    Dead,
}

public enum WildBoarSkillEnum
{
    Rush
}

public class WildBoar : Enemy<WildBoarEnum>
{
    public WildBoarSkill Skills { get; private set; }

    public GameObject dashAttackCollider; 
    private Transform _playerTrm;

    private SkillSO _rushSkill;

    private float _attackDelay;
    public void SetAttackDelay(float delay) => _attackDelay = Time.time + delay;


    protected override void Awake()
    {
        base.Awake();
        _playerTrm = PlayerManager.Instance.PlayerTrm;

        detectingDistance = EnemyStat.detectingDistance.GetValue();
    }

    private void OnEnable()
    {
        healthCompo.OnKnockBack += KnockBack;
        healthCompo.OnHit += OnHit;
        healthCompo.OnDie += OnDie;
    }

    private void OnDisable()
    {
        healthCompo.OnKnockBack -= KnockBack;
        healthCompo.OnHit -= OnHit;
        healthCompo.OnDie -= OnDie;
    }

    private void Start()
    {
        StateMachine.Initialize(WildBoarEnum.Idle, this);
        //attackDistance = Skills.GetSkillByEnum(WildBoarSkillEnum.Rush).attackDistance.GetValue();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.UpdateState();
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void Stun(float duration)
    {
        if (IsDead) return;
        base.Stun(duration);

        if (StateMachine.CurrentState is WildBoarRushState)
            CanStateChangeable = true;
        else if (StateMachine.CurrentState is WildBoarStunState)
            return;
        StateMachine.ChangeState(WildBoarEnum.Stun);
    }

    public override void Stone(float duration)
    {
        if (IsDead) return;
        base.Stone(duration);
        animatorCompo.speed = 0;
    }

    public override void Dead(Vector2 dir) { }

    public override void AirBorn(float duration)
    {
        if (IsDead) return;
        base.AirBorn(duration);
        //StateMachine.ChangeState(WildBoarEnum.AirBorn);
    }

    public override void SetIdle()
    {
        StateMachine.ChangeState(WildBoarEnum.Idle);
    }

    public void Attack()
    {

    }

    private void OnHit()
    {
        //HitEvent?.Invoke();
        StateMachine.ChangeState(WildBoarEnum.Idle);
    } 

    private void OnDie(Vector2 dir)
    {
        for (int i = 0; i < EnemyStat.dropItems.Count; i++)
        {
            if (UnityEngine.Random.Range(0, 101) < EnemyStat.dropItems[i].appearChance)
            {
                DropItem dropItem = Instantiate(EnemyStat.dropItems[i].dropItemPf).GetComponent<DropItem>();
                dropItem.transform.position = transform.position + Vector3.up;
                dropItem.SpawnItem(dir);
            }
        }

        CanStateChangeable = true;
        StateMachine.ChangeState(WildBoarEnum.Dead);
        CanStateChangeable = false;
        IsDead = true;
    }
}
