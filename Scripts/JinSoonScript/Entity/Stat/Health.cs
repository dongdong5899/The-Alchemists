using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, IDamageable
{
    public Entity owner { get; private set; }

    public Stat maxHp { get; private set; }
    public float weight { get; private set; }
    public float curHp;

    public HitData hitData { get; private set; }

    public Stat maxArmor { get; private set; }
    public float curArmor { get; private set; }
    public float lastAttackDamage { get; private set; }
    public bool isLastAttackCritical { get; private set; }

    public bool isInvincible { get; private set; }

    //효과, 지속시간, 시작된 시간
    //protected List<Tuple<Effect, float, float>> effects = new List<Tuple<Effect, float, float>>();

    public Action OnHit;
    public Action OnHeal;
    public Action OnCrit;
    public Action<Vector2> OnKnockBack;
    public Action<Vector2> OnDie;


    private void Awake()
    {
        owner = GetComponent<Entity>();
        Init();
    }

    public void Init()
    {
        weight = owner.Stat.weight;
        maxHp = owner.Stat.maxHp;
        maxArmor = owner.Stat.maxHp;
        curHp = maxHp.GetValue();
        curArmor = maxArmor.GetValue();
        hitData = new HitData();
    }

    #region HealthRegion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="knockPower"></param>
    /// <param name="dealer"></param>
    public virtual bool TakeDamage(int damage, Vector2 knockPower, Entity dealer, bool isPercent = false)
    {
        if (owner.IsDead || isInvincible) return true;
        if (owner.IsConstParrying) return false; 
        

        if (isPercent)
        {
            damage = (int)(maxHp.GetValue() * ((float)damage / 100));
        }

        //크리티컬 계산
        if (dealer != null)
        {
            damage = (int)(damage * dealer.Stat.globalDamageInflict.GetValue());

            float criticalChance = dealer.Stat.criticalChance.GetValue() * 100;
            bool isCrit = Random.Range(0, 10001) <= criticalChance;

            if (isCrit)
            {
                OnCrit?.Invoke();
                damage = (int)(damage * (dealer.Stat.criticalDamage.GetValue() / 100));
            }
            hitData.isLastAttackCritical = isCrit;
        }

        damage = (int)((damage + owner.Stat.damageReceiv.GetValue()) * owner.Stat.damageReceivPercent.GetValue());

        if (damage < 0)
            damage = 0;

        //hitData에 맞았다라고 저장
        hitData.lastAttackDamage = damage;
        hitData.lastAttackEntity = dealer;
        hitData.lastHitTime = Time.time;

        //hp감소시켜
        curHp -= damage;
        curHp = Mathf.Clamp(curHp, 0, maxHp.GetValue());
        OnHit?.Invoke();

        //무게로 나눠 무게가 1이면 그대로, 2면 1/2로 날라감
        knockPower /= weight;
        AfterHitFeedback(knockPower, dealer, true);

        return true;
    }

    private void AfterHitFeedback(Vector2 knockPower, Entity dealer, bool withFeedBack = true)
    {
        if (withFeedBack)
        {
            OnKnockBack?.Invoke(knockPower);
        }
        if (curHp <= 0)
            OnDie?.Invoke(knockPower);

        if (dealer is Player player)
        {
            player.OnKilled?.Invoke(owner);
        }
    }

    public void GetHeal(int amount)
    {
        amount = (int)(amount * owner.Stat.recoveryReceive.GetValue());
        curHp += amount;
        curHp = Mathf.Clamp(curHp, 0, maxHp.GetValue());
        OnHeal?.Invoke();
    }

    public void GetArmor(int amount)
    {
        curArmor += amount;
        curArmor = Math.Clamp(curArmor, 0, maxArmor.GetValue());
    }

    public void LostArmor(int amount)
    {
        curArmor -= amount;
        curArmor = Math.Clamp(curArmor, 0, maxArmor.GetValue());
    }

    public void ReduceMaxHp(float amount)
    {
        amount = -Mathf.Clamp(amount, 0f, maxHp.GetValue() - 1);
        maxHp.AddModifier(amount);
    }

    public void EnableInvincibility()
    {
        isInvincible = true;
    }

    public void DisableInvincibility()
    {
        isInvincible = false;
    }

    #endregion

    #region EffectRegion

    private void Update()
    {
        //for (int i = 0; i < effects.Count; i++)
        //{
        //    Tuple<Effect, float, float> effect = effects[i];
        //    effect.Item1.UpdateEffort();

        //    if (effect.Item3 + effect.Item2 < Time.time)
        //    {
        //        effect.Item1.ExitEffort();
        //        effects.Remove(effect);
        //    }
        //}
    }


    //public virtual void GetEffort(Effect effect)
    //{
    //    for (int i = 0; i < effects.Count; i++)
    //    {
    //        var item = effects[i];
    //        if (item.Item1 == effect)
    //        {
    //            float remainDuration = item.Item2;
    //            effects[i] = new Tuple<Effect, float, float>(item.Item1, remainDuration + effect.duration, item.Item3);
    //            return;
    //        }
    //    }

    //    effects.Add(new Tuple<Effect, float, float>(effect, effect.duration, Time.time));
    //    effect.EnterEffort(owner);
    //}

    //public void Rape(float time)
    //{
    //    KingSlime slime = owner as KingSlime;
    //    if (slime)
    //    {
    //        slime.StateMachine.ChangeState(KingSlimeStateEnum.Vined);
    //        StartCoroutine(DelayRapeOff(slime, time));
    //    }
    //}

    //IEnumerator DelayRapeOff(KingSlime slime, float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    slime.CanStateChangeable = true;
    //    slime.StateMachine.ChangeState(KingSlimeStateEnum.Ready);
    //}


    #endregion
}

public class HitData
{
    public float lastHitTime;
    public float lastAttackDamage;
    public bool isLastAttackCritical;
    public Entity lastAttackEntity;
}