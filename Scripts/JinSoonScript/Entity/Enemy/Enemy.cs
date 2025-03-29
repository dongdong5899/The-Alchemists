using DG.Tweening;
using System;
using UnityEngine;
public abstract class Enemy<T> : Entity where T : Enum
{
    public EnemyStateMachine<T> StateMachine { get; private set; }
    public EnemyStatSO EnemyStat { get; private set; }

    #region EnemyStat
    public float PatrolTime { get => EnemyStat.patrolTime.GetValue(); protected set => EnemyStat.patrolTime.SetDefaultValue(value); }
    public float PatrolDelay { get => EnemyStat.patrolDelay.GetValue(); protected set => EnemyStat.patrolDelay.SetDefaultValue(value); }
    public float detectingDistance { get => EnemyStat.detectingDistance.GetValue(); protected set => EnemyStat.detectingDistance.SetDefaultValue(value); }
    public float attackDistance { get => EnemyStat.attackDistance.GetValue(); protected set => EnemyStat.attackDistance.SetDefaultValue(value); }

    #endregion

    #region Settings

    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected LayerMask whatIsObstacle;

    [Header("Attack Settings")]
    public float runAwayDistance;
    public float attackCool;
    [HideInInspector] public float lastAttackTime;

    #endregion

    public float defaultMoveSpeed { get; protected set; }
    protected int lastAnimationBoolHash;

    protected override void Awake()
    {
        base.Awake();
        EnemyStat = Stat as EnemyStatSO;
        defaultMoveSpeed = EnemyStat.moveSpeed.GetValue();

        StateMachine = new EnemyStateMachine<T>();
        foreach (T stateEnum in Enum.GetValues(typeof(T)))
        {
            string typeName = stateEnum.ToString();
            string scriptName = GetType().ToString();
            Type t = Type.GetType($"{scriptName}{typeName}State");

            try
            {
                var enemyState = Activator.CreateInstance(type: t, this, StateMachine, typeName) as EnemyState<T>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError($"Enemy {scriptName} : no state [ {typeName} ]");
                Debug.LogError($"{scriptName}{typeName}State");
                Debug.LogError(e);
#endif
            }
        }
        healthCompo.OnHit += OnHit;
    }

    private void OnDestroy()
    {
        healthCompo.OnHit -= OnHit;
    }

    private void OnHit()
    {
        AudioManager.Instance.PlaySound(SoundEnum.EnemyHit, transform.position);
        HitEvent?.Invoke();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.UpdateState();
    }

    #region DetectRegion


    /// <summary>
    /// 플레이어를 바라보고, detectingDistance 만큼 가까이 있으면 아항♡
    /// </summary>
    /// <returns></returns>
    public virtual Player IsPlayerDetected()
    {
        if (PlayerManager.Instance.Player.isNatureSync) return null;

        Collider2D player = Physics2D.OverlapCircle(transform.position, detectingDistance, whatIsPlayer);
        if (player == null)
            return null;

        Player playerCompo = player.GetComponent<Player>();
        float dist = Vector2.Distance(player.transform.position, transform.position);

        if (dist < detectingDistance) return playerCompo;
        else return null;

        //float dir = player.transform.position.x - transform.position.x;

        //if (dir > 0 == (FacingDir > 0)) return playerCompo;
        //else return null;
    }
    public virtual Player IsPlayerInAttackRange(float width = -1)
    {
        if (PlayerManager.Instance.Player.isNatureSync) return null;

        Collider2D player = Physics2D.OverlapCircle(transform.position, attackDistance, whatIsPlayer);
        Collider2D subPlayer = null;
        if (width != -1)
            subPlayer = Physics2D.OverlapCircle(transform.position, attackDistance - width, whatIsPlayer);
        if (player == null || subPlayer != null)
            return null;

        Player playerCompo = player.GetComponent<Player>();

        return playerCompo;
    }

    /// <summary>
    /// 플레이어까지 직선상에 장애물이있는지
    /// </summary>
    /// <param name="distance">플레이어까지의 거리</param>
    /// <returns></returns>
    public virtual bool IsObstacleInLine(float distance)
    {
        Vector2 dir = ((PlayerManager.Instance.PlayerTrm.position + Vector3.up) - transform.position).normalized;

        Debug.DrawRay(transform.position, dir, Color.red);

        return Physics2D.Raycast(transform.position, dir, distance, whatIsObstacle);
    }

    /// <summary>
    /// 진행방향 앞쪽에 바닥이있는지 확인
    /// </summary>
    /// <returns></returns>
    public bool IsFrontGround() => Physics2D.Raycast(wallChecker.position, Vector2.down, 5f, whatIsGroundAndWall);
    #endregion

    public void OnCompletelyDie()
    {
        //풀링 하면 여기에다가 추가해주면 도미 
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
