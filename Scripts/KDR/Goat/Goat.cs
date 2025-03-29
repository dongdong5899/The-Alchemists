using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum GoatEnum
{
    Idle, 
    Attack,
    Patrol,
    Chase,
    Stun,
    AirBorn,
    Dead,
}

public class Goat : Enemy<GoatEnum>
{
    private readonly int _YVelocityAnimHash = Animator.StringToHash("YVelocity");
    private readonly int _IsGroundAnimHash = Animator.StringToHash("IsGround");


    protected override void Awake()
    {
        base.Awake();

        AudioManager.Instance.PlaySound(SoundEnum.SlimeMove, transform);

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
        StateMachine.Initialize(GoatEnum.Idle, this);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.UpdateState();

        animatorCompo.SetFloat(_YVelocityAnimHash, MovementCompo.RigidbodyCompo.velocity.y);
        animatorCompo.SetBool(_IsGroundAnimHash, IsGroundDetected());
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void Stun(float duration)
    {
        if (IsDead) return;
        base.Stun(duration);
        if (StateMachine.CurrentState is GoatStunState)
            return;
        StateMachine.ChangeState(GoatEnum.Stun);
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
        StateMachine.ChangeState(GoatEnum.Idle);
    }

    public override void Dead(Vector2 dir)
    {

    }

    private void OnHit()
    {
        //HitEvent?.Invoke();
        StateMachine.ChangeState(GoatEnum.Idle);
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
        StateMachine.ChangeState(GoatEnum.Dead);
        CanStateChangeable = false;
        IsDead = true;
    }
}
