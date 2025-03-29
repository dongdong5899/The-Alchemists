using System;
using UnityEngine;

public enum PlayerStateEnum
{
    Idle,
    Move,
    Jump,
    Fall,
    Dash,
    Gathering,
    Stun,
    NormalAttack,
    Climb,
    Throw,
    Dead,
    Push,
}

public enum PlayerSkillEnum
{
    Dash,
    NormalAttack
}

public class Player : Entity
{
    public PlayerSkill SkillSO { get; private set; }

    #region Status

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        private set { }
    }
    public float JumpForce { get; protected set; } = 10f;

    #endregion

    #region DashInfo

    public float DashTime { get; private set; }
    public float DashPower { get; private set; }
    public bool IsInvincibleWhileDash { get; private set; }
    public bool IsAttackWhileDash { get; private set; }

    #endregion

    #region ComponentRegion

    public PlayerStateMachine StateMachine { get; private set; }
    [SerializeField] private InputReader _inputReader;
    public InputReader PlayerInput => _inputReader;

    #endregion

    #region CoyoteTime

    [SerializeField] private float coyoteTime = 0.3f;
    public float CoyoteTime => coyoteTime;

    private bool canJump = false;
    [HideInInspector]
    public bool CanJump
    {
        get
        {
            return curJumpCnt < maxJumpCnt;
        }
        set
        {
            canJump = value;
        }
    }

    #endregion

    #region Attack

    [HideInInspector] public int ComboCounter = 0;
    [HideInInspector] public float lastAttackTime;

    #endregion

    [field: SerializeField]
    public Transform PlayerCenter { get; private set; }

    [SerializeField]
    private LayerMask _whatIsPush;

    public int maxJumpCnt { get; private set; } = 2;
    [HideInInspector] public int curJumpCnt = 0;

    public GameObject StunEffect => _stunSprite;

    private bool isInventoryOpen = false;
    public bool canClimb { get; private set; } = false;

    [SerializeField] private HpBar _hpDecator;
    [SerializeField] private GameObject thowingPortionPf;
    [SerializeField] private Vector3 _throwingOffset;
    public Vector3 ThrowingOffset { get => _throwingOffset; private set => _throwingOffset = value; }

    [SerializeField]
    private float _objectCheckDistance = 0.5f;

    [HideInInspector] public bool throwingPortionSelected = false;
    [HideInInspector] public Vector2 PortionThrowingDir;
    [HideInInspector] public bool canDash = false;

    [HideInInspector] public bool isNatureSync = false;
    [HideInInspector] public bool canAttackWithNatureSync;

    [field: SerializeField]
    public Transform CurrentPushTrm { get; set; }
    public GrowingGrass CurrentVine { get; set; }

    public Action<Entity> OnKilled;

    public GameObject testObject;
    public Spike spike;
    public HornSpike hornSpike;
    public Boom boom;


    protected override void Awake()
    {
        Debug.Log("===========PlayerAwake");
        base.Awake();
        Debug.Log(animatorCompo);
        _inputReader.Controlls.Player.Enable();
        //canDash = true;
        _hpDecator?.Init((int)healthCompo.curHp);

        MoveSpeed = moveSpeed;
        JumpForce = Stat.jumpForce.GetValue();

        SkillSO = gameObject.AddComponent<PlayerSkill>();
        SkillSO.Init(EntitySkillSO);

        StateMachine = new PlayerStateMachine();
        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{typeName} is loading error!");
                Debug.LogError(ex);
            }
        }

        foreach (var item in EntitySkillSO.skills)
            item.skill.SetOwner(this);
    }

    private void OnEnable()
    {
        _inputReader.PressTabEvent += InventoryOpen;
        _inputReader.OpenOptionEvent += OpenOption;
        healthCompo.OnKnockBack += KnockBack;
        healthCompo.OnDie += OnDie;
        healthCompo.OnHit += OnHit;
    }

    private void OnDisable()
    {
        _inputReader.PressTabEvent -= InventoryOpen;
        _inputReader.OpenOptionEvent -= OpenOption;
        healthCompo.OnKnockBack -= KnockBack;
        healthCompo.OnDie -= OnDie;
        healthCompo.OnHit -= OnHit;
        StateMachine.CurrentState.Exit();
    }

    protected void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    protected override void Update()
    {
        base.Update();

        if (_inputReader.Controlls.Player.enabled == false && Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySound(SoundEnum.Click, transform);
        }

        StateMachine.CurrentState.UpdateState();
        CheckObjectOnFoot();
    }

    public override void Stun(float duration)
    {
        base.Stun(duration);
        stunEndTime = duration;
        StateMachine.ChangeState(PlayerStateEnum.Stun);
    }

    //public override void UpArmor(float figure)
    //{

    //}



    public override void Invincibility(float duration)
    {
        base.Invincibility(duration);
        //StateMachine.ChangeState(PlayerStateEnum.Stun);
    }

    public override void InvincibilityDisable()
    {
        base.InvincibilityDisable();
        StateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void Clean()
    {
        base.Clean();
        StateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    /// <summary>
    /// 대쉬하는
    /// </summary>
    /// <param name="dashTime">대쉬하는 시간</param>
    /// <param name="dashPower">대쉬하는 속도</param>
    /// <param name="isInvincibleWhileDash">대쉬하는 동안 무적인가</param>
    /// <param name="isAttackWhileDash">대쉬공격하는가</param>
    public void Dash(float dashTime, float dashPower, bool isInvincibleWhileDash = false, bool isAttackWhileDash = false)
    {
        this.DashTime = dashTime;
        this.DashPower = dashPower;
        this.IsInvincibleWhileDash = isInvincibleWhileDash;
        this.IsAttackWhileDash = isAttackWhileDash;

        StateMachine.ChangeState(PlayerStateEnum.Dash);
    }

    /// <summary>
    /// 인벤토리 열때
    /// 움직이는거 다 빼뒀다가 나중에 다시 넣어주
    /// </summary>
    private void InventoryOpen()
    {
        if (isInventoryOpen == false)
        {
            PlayerManager.Instance.DisablePlayerMovementInput();
            UIManager.Instance.Open(UIType.Inventory);
            isInventoryOpen = true;
        }
        else
        {
            PlayerManager.Instance.EnablePlayerMovementInput();
            UIManager.Instance.Close(UIType.Inventory);
            isInventoryOpen = false;
        }
    }

    private void OpenOption()
    {
        Option option = UIManager.Instance.GetUI(UIType.Option) as Option;
        if (option.isOpened)
        {
            option.Close();
        }
        else
        {
            option.Open();
        }
    }



    public void OnHit()
    {
        AudioManager.Instance.PlaySound(SoundEnum.PlayerHit, transform.position);

        HitEvent?.Invoke();
    }

    public void OnDie(Vector2 hitDir)
    {
        CanStateChangeable = true;
        PlayerManager.Instance.DisablePlayerMovementInput();
        PlayerManager.Instance.DisablePlayerInventoryInput();
        StateMachine.ChangeState(PlayerStateEnum.Dead);
        CanStateChangeable = false;
        IsDead = true;
    }

    //public void ThrowPortion(PortionItem portion)
    //{
    //    Vector3 spawnPosition = transform.position + ThrowingOffset;
    //    ThrowingPortion throwingPortion =
    //        Instantiate(thowingPortionPf, spawnPosition, Quaternion.identity)
    //        .GetComponent<ThrowingPortion>();

    //    throwingPortion.Init(portion);
    //}

    //public void WeaponEnchant(PortionItem portion)
    //{

    //}

    public void Climb(bool b) 
        => canClimb = b;

    public void CheckOneWayPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundChecker.position,
            new Vector2(groundCheckBoxWidth, 0.05f), 0,
            Vector2.down, groundCheckDistance, whatIsGroundAndWall);

        if (hit.collider.TryGetComponent(out OneWayPlatform p))
        {
            p.Interact(this);
        }
    }

    public virtual Transform CheckObjectInFront()
    {
        RaycastHit2D hit = Physics2D.BoxCast(wallChecker.position,
            new Vector2(0.05f, wallCheckBoxHeight), 0,
            Vector2.right * FacingDir, _objectCheckDistance, _whatIsPush);

        return hit.transform;
    }

    

    public void SetActiveInput(bool isActive)
    {
        if (isActive)
            PlayerInput.Controlls.Player.Enable();
        else
            PlayerInput.Controlls.Player.Disable();
    }

    public override void AnimationTrigger(AnimationTriggerEnum trigger)
    {
        base.AnimationTrigger(trigger);
        StateMachine.CurrentState.AnimationTrigger(trigger);
    }
}
