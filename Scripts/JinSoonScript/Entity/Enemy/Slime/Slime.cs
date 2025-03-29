using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum SlimeStateEnum
{
    Idle,
    Patrol,
    Chase,
    JumpAttack,
    Stun,
    AirBorn,
    Dead
}

public enum SlimeSkillEnum
{
    JumpAttack
}

public class Slime : Enemy<SlimeStateEnum>
{
    public SlimeSkill Skills { get; private set; }


    [HideInInspector] public bool moveAnim = false;


    protected override void Awake()
    {
        base.Awake();

        AudioManager.Instance.PlaySound(SoundEnum.SlimeMove, transform);

        Skills = gameObject.AddComponent<SlimeSkill>();
        Skills.Init(EntitySkillSO);

        foreach (var item in EntitySkillSO.skills)
        {
            item.skill.SetOwner(this);
        }

        PatrolTime = EnemyStat.patrolTime.GetValue();
        PatrolDelay = EnemyStat.patrolDelay.GetValue();
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

    protected void Start()
    {
        StateMachine.Initialize(SlimeStateEnum.Idle, this);
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
        if (StateMachine.CurrentState is SlimeStunState)
            return;
        StateMachine.ChangeState(SlimeStateEnum.Stun);
    }

    public override void Stone(float duration)
    {
        if (IsDead) return;
        base.Stone(duration);
        animatorCompo.speed = 0;
    }

    public override void AirBorn(float duration)
    {
        if (IsDead) return;
        base.AirBorn(duration);
        //StateMachine.ChangeState(SlimeStateEnum.AirBorn);
    }

    public override void SetIdle()
    {
        StateMachine.ChangeState(SlimeStateEnum.Idle);
    }

    public override void Dead(Vector2 dir)
    {

    }

    private void OnHit()
    {
        //HitEvent?.Invoke();
        StateMachine.ChangeState(SlimeStateEnum.Idle);
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
        StateMachine.ChangeState(SlimeStateEnum.Dead);
        CanStateChangeable = false;
        IsDead = true;
    }
}
