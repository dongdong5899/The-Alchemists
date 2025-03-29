using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, IAffectable, IAnimationTriggerable
{
    #region ComponentRegion
    [field:SerializeField] public EntityStatSO Stat { get; private set; }
    [SerializeField] private EntitySkillSO entitySkillSO;
    public EntitySkillSO EntitySkillSO => entitySkillSO;

    public Transform visualTrm { get; protected set; }

    public EntityVisual visualCompo { get; protected set; }
    public Animator animatorCompo { get; protected set; }
    public SpriteRenderer spriteRendererCompo { get; protected set; }
    public Collider2D colliderCompo { get; protected set; }
    public Rigidbody2D rigidbodyCompo { get; protected set; }
    public EntityMovement MovementCompo { get; protected set; }

    public EntityAttack entityAttack { get; protected set; }

    public Health healthCompo { get; protected set; }
    #endregion

    [Header("Collision info")]
    [SerializeField] protected LayerMask whatIsGroundAndWall;
    [SerializeField] protected LayerMask whatIsProbs;
    [SerializeField] protected LayerMask whatIsSpikeTrap;
    [Space(10)]
    [SerializeField] protected Transform groundChecker;
    [SerializeField] protected float groundCheckBoxWidth;
    [SerializeField] protected float groundCheckDistance;
    [Space(10)]
    [SerializeField] protected Transform wallChecker;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float wallCheckBoxHeight;

    private float _gravityScale;
    public float moveSpeed => Stat.moveSpeed.GetValue();
    protected float knockbackDuration = 0.1f;
    protected Coroutine knockbackCoroutine;
    public bool isKnockbacked { get; protected set; }

    public float stunEndTime { get; protected set; }
    public bool canBeStun { get; protected set; }
    public float airBornDuration { get; protected set; }
    public bool canBeAirBorn { get; protected set; }
    public float upArmorDuration { get; protected set; }

    protected int _buffStatusEffectBit = 0;
    protected int _debuffStatusEffectBit = 0;
    public event Action<int, int, bool, bool> OnStatusChanged;

    [Space]
    [Header("FeedBack info")]
    public UnityEvent HitEvent;
    protected GameObject _stunSprite;
    protected GameObject _shieldSprite;

    public Action<int> OnFlip;
    public int FacingDir { get; protected set; } = 1;
    public bool IsDead { get; protected set; } = false;
    public bool CanStateChangeable = true;
    public bool CanKnockback = true;
    public bool IsConstParrying = false;
    public int parryingLevel = 0;

    private int _animationTriggerBit = 0;
    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    protected virtual void Awake()
    {
        _stunSprite = transform.Find("StunSprite").gameObject;
        _shieldSprite = transform.Find("ShieldSprite").gameObject;
        visualTrm = transform.Find("Visual");
        visualCompo = visualTrm.GetComponent<EntityVisual>();
        animatorCompo = visualTrm.GetComponent<Animator>();
        spriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        rigidbodyCompo = GetComponent<Rigidbody2D>();
        colliderCompo = GetComponent<Collider2D>();
        healthCompo = GetComponent<Health>();
        entityAttack = GetComponent<EntityAttack>();
        MovementCompo = GetComponent<EntityMovement>();
        MovementCompo.Initialize(this);

        Stat = Instantiate(Stat);
        entitySkillSO = ScriptableObject.Instantiate(entitySkillSO);

        _statusEffectManager = new StatusEffectManager(this);

        _gravityScale = rigidbodyCompo.gravityScale;
    }

    private void FixedUpdate()
    {
        //rigidbodyCompo.velocity = new Vector2(_xMovement, rigidbodyCompo.velocity.y);
    }

    protected virtual void Update()
    {
        _statusEffectManager.UpdateStatusEffects();

        CheckSpikeTrap();
    }

    #region SpikeTrapDamage
    private float _lastSpikeTrapDamageTime;
    private float _spikeTrapDamageCool = 0.5f;
    private int _spikeTrapDamage = 1;
    private Collider2D[] _spikeColl = new Collider2D[1];
    private void CheckSpikeTrap()
    {
        BoxCollider2D boxColl = colliderCompo as BoxCollider2D;
        Vector2 size = boxColl.size;
        Vector3 offset = boxColl.offset;
        
        if (Physics2D.OverlapBoxNonAlloc(transform.position + offset,
            size + Vector2.one * 0.2f, 0, _spikeColl, whatIsSpikeTrap) != 0 &&
            _lastSpikeTrapDamageTime + _spikeTrapDamageCool < Time.time)
        {
            _lastSpikeTrapDamageTime = Time.time;

            healthCompo.TakeDamage(_spikeTrapDamage, Vector2.zero, null);
        }
    }
    #endregion

    #region Velocity Section

    /// <summary>
    /// Set velocity zero this Entity
    /// </summary>
    /// <param name="withYAxis">Stop With YAxis</param>

    #endregion

    #region FlipSection

    public virtual void Flip()
    {
        FacingDir = FacingDir * -1;
        visualTrm.Rotate(0, 180f, 0);
        OnFlip?.Invoke(FacingDir);
    }

    public virtual void FlipController(float x)
    {
        if (Mathf.Abs(x) < 0.05f) return;
        x = Mathf.Sign(x); //x 의 부호만 가져오거든
        if (Mathf.Abs(FacingDir + x) < 0.5f)
            Flip();
    }

    #endregion

    #region CheckCollisionSection

    public virtual bool IsGroundDetected(Vector3? offset = null, float distance = -1)
    {
        Vector3 acturalOffset = offset ?? Vector3.zero;
        return Physics2D.BoxCast(groundChecker.position + acturalOffset,
            new Vector2(groundCheckBoxWidth, 0.05f), 0,
            Vector2.down, distance != -1 ? distance : groundCheckDistance, whatIsGroundAndWall);

    }


    public virtual bool IsWallDetected(float yOffset = 0, float distance = -1) =>
        Physics2D.BoxCast(wallChecker.position + Vector3.up * yOffset,
            new Vector2(0.05f, wallCheckBoxHeight), 0,
            Vector2.right * FacingDir, distance == -1 ? wallCheckDistance : distance, whatIsGroundAndWall);

    public virtual bool IsWallDetected(out Collider2D detectedCollider, float yOffset = 0, float distance = -1)
    {
        RaycastHit2D hit = Physics2D.BoxCast(wallChecker.position + Vector3.up * yOffset,
            new Vector2(0.05f, wallCheckBoxHeight), 0,
            Vector2.right * FacingDir, distance == -1 ? wallCheckDistance : distance, whatIsGroundAndWall);
        detectedCollider = hit.collider;
        return hit.collider != null;
    }
        

    public virtual void CheckObjectOnFoot()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundChecker.position,
            new Vector2(groundCheckBoxWidth, 0.05f), 0,
            Vector2.down, groundCheckDistance, whatIsProbs);

        if (hit.collider != null && hit.collider.TryGetComponent(out Probs p))
            p.Interact(this);
    }

    #endregion

    public void SetGravityActive(bool isActive)
    {
        float gravityScale = isActive ? _gravityScale : 0;
        rigidbodyCompo.gravityScale = gravityScale;
    }

    public virtual void Dead(Vector2 dir) { }

    public virtual void KnockBack(Vector2 power)
    {
        if (CanKnockback == false) return;
        MovementCompo.StopImmediately(true);
        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);

        isKnockbacked = true;
        MovementCompo.SetVelocity(power, true, true);
        MovementCompo.canSetVelocity = false;
        knockbackCoroutine = StartDelayCallBack(
            knockbackDuration, () =>
            {
                isKnockbacked = false;
                MovementCompo.canSetVelocity = true;
                MovementCompo.StopImmediately(true);
            });
    }

    public virtual void Stun(float duration)
    {
        stunEndTime = Mathf.Max(duration + Time.time, stunEndTime);
    }
    public void OnStunSprite(bool active)
    {
        _stunSprite.SetActive(active);
    }
    public void OnShieldEffect(bool active)
    {
        _shieldSprite.SetActive(active);
    }
    public virtual void Stone(float duration)
    {
        Stun(duration);
        visualCompo.OnStone(true);
        healthCompo.OnHit += StonEffect;
        StartDelayCallBack(duration, () =>
        {
            healthCompo.OnHit -= StonEffect;
            visualCompo.OnStone(false);
        });
    }
    private void StonEffect()
    {
        Transform effectTrm = Instantiate(EffectInstantiateManager.Instance.stonHitEffect, transform.position, Quaternion.identity).transform;
        effectTrm.localScale = Vector3.one * groundCheckBoxWidth;
    }

    public virtual void AirBorn(float duration)
    {
        airBornDuration = Mathf.Max(airBornDuration, duration);
        Stun(duration);
    }

    public virtual void SetIdle() { }

    public virtual void UpArmor(int figure)
    {
        healthCompo.GetArmor(figure);
    }

    public virtual void LostArmor(int figure)
    {
        healthCompo.LostArmor(figure);
    }

    public virtual void Invincibility(float duration)
    {
        healthCompo.EnableInvincibility();
    }

    public virtual void InvincibilityDisable()
    {
        healthCompo.DisableInvincibility();
    }

    public virtual void Clean()
    {
        Debug.Log("Entity에서 실행된 Clean");
        CleanDamageManager.Instance.DamageObject();

        if (stunEndTime > 0)
        {
            stunEndTime = 0;
            canBeStun = false;
            Debug.Log("스턴 해제됨");
        }
    }

    //얘가 막 몇초 후 실행 시키기 그런걸 다 관리 해줄 거임
    public Coroutine StartDelayCallBack(float delay, Action callBack)
    {
        return StartCoroutine(DelayCoroutine(delay, callBack));
    }

    IEnumerator DelayCoroutine(float delay, Action callBack)
    {
        yield return new WaitForSeconds(delay);
        callBack?.Invoke();
    }

    public StatusEffect ApplyStatusEffect(StatusBuffEffectEnum statusEffect, int level, float duration)
    {
        if (IsUnderStatusEffect(statusEffect)) return null;

        _buffStatusEffectBit |= (int)statusEffect;
        OnStatusChanged?.Invoke((int)statusEffect, level, true, true);
        return _statusEffectManager.AddStatusEffect(statusEffect, level, duration);
    }
    public StatusEffect ApplyStatusEffect(StatusDebuffEffectEnum statusEffect, int level, float duration)
    {
        if (IsUnderStatusEffect(statusEffect)) return null;

        if (EffectInstantiateManager.Instance.statusDebuffEffectColor.ContainsKey(statusEffect))
        {
            ParticleSystem ps = Instantiate(EffectInstantiateManager.Instance.statusEffect, transform);
            ps.transform.transform.localPosition = Vector3.zero;
            ps.transform.localScale = Vector3.one * groundCheckBoxWidth * 0.7f;
            var mainModule = ps.main;
            mainModule.duration = duration;
            mainModule.startColor = EffectInstantiateManager.Instance.statusDebuffEffectColor[statusEffect];
        }
         
        _debuffStatusEffectBit |= (int)statusEffect;
        OnStatusChanged?.Invoke((int)statusEffect, level, false, true);
        return _statusEffectManager.AddStatusEffect(statusEffect, level, duration);
    }

    public void RemoveStatusEffect(StatusBuffEffectEnum statusEffect)
    {
        if (IsUnderStatusEffect(statusEffect) == false) return;
        _buffStatusEffectBit &= ~(int)statusEffect;
        OnStatusChanged?.Invoke((int)statusEffect, 0, true, false);
    }
    public void RemoveStatusEffect(StatusDebuffEffectEnum statusEffect)
    {
        if (IsUnderStatusEffect(statusEffect) == false) return;
        _debuffStatusEffectBit &= ~(int)statusEffect;
        OnStatusChanged?.Invoke((int)statusEffect, 0, false, false);
    }

    public bool IsUnderStatusEffect(StatusBuffEffectEnum statusEffect)
        => (_buffStatusEffectBit & (int)statusEffect) != 0;
    public bool IsUnderStatusEffect(StatusDebuffEffectEnum statusEffect)
        => (_debuffStatusEffectBit & (int)statusEffect) != 0;

    public virtual void ApplyEffect()
    {
    }

    public virtual void AnimationTrigger(AnimationTriggerEnum trigger)
    {
        _animationTriggerBit |= (int)trigger;
    }

    public virtual bool IsTriggered(AnimationTriggerEnum trigger) => (_animationTriggerBit & (int)trigger) != 0;

    public virtual void RemoveTrigger(AnimationTriggerEnum trigger)
    {
        _animationTriggerBit &= ~(int)trigger;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(wallChecker.position,
            new Vector2(wallCheckDistance, wallCheckBoxHeight));
        Gizmos.DrawWireCube(groundChecker.position,
            new Vector2(groundCheckBoxWidth, groundCheckDistance));
    }
#endif
}
