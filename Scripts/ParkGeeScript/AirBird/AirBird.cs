using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum AirBirdEnum
{
    Idle,
    Patrol,
    Chase,
    Shoot,
    Stun,
    AirBorn,
    Dead
}

public enum AirBirdSkillEnum
{
    Shoot
}

public class AirBird : Enemy<AirBirdEnum>
{
    public Feather featherPrefab;
    public float featherShootSpeed = 12f;

    protected override void Awake()
    {
        base.Awake();

        AudioManager.Instance.PlaySound(SoundEnum.BirdSound, transform);
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
        StateMachine.Initialize(AirBirdEnum.Idle, this);
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
        if (StateMachine.CurrentState is AirBirdStunState)
            return;
        StateMachine.ChangeState(AirBirdEnum.Stun);
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
        MovementCompo.canSetVelocity = true;
        base.AirBorn(duration);
        //StateMachine.ChangeState(AirBirdEnum.AirBorn);
    }

    public override void SetIdle()
    {
        StateMachine.ChangeState(AirBirdEnum.Idle);
    }

    public override void Dead(Vector2 dir)
    {

    }

    private void OnHit()
    {
        //HitEvent?.Invoke();
        StateMachine.ChangeState(AirBirdEnum.Idle);
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
        StateMachine.ChangeState(AirBirdEnum.Dead);
        CanStateChangeable = false;
        IsDead = true;
    }
}
