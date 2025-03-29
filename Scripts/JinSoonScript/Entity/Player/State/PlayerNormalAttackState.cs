using UnityEngine;

public class PlayerNormalAttackState : PlayerState
{
    private int comboCounterHash;
    private PlayerNormalAttackSO playerNormalAttackSO;
    private EntityAttack playerAttack;

    public PlayerNormalAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        comboCounterHash = Animator.StringToHash("ComboCounter");
        playerNormalAttackSO = player.SkillSO.GetSkillByEnum(PlayerSkillEnum.NormalAttack) as PlayerNormalAttackSO;
        playerAttack = player.GetComponent<EntityAttack>();
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.Instance.PlaySound(SoundEnum.PlayerAttack, player.transform);

        if (player.canAttackWithNatureSync == false)
            player.isNatureSync = false;

        //�ð��� ������ �����ϸ� �޺�ī���� �ʱ�ȭ
        if (player.lastAttackTime + playerNormalAttackSO.attackComboDragTime <= Time.time)
            player.ComboCounter = 0;

        AttackInfo attackInfo = playerNormalAttackSO.attackInfos[player.ComboCounter];
        attackInfo.damage =
            (int)(attackInfo.attackMultiplier * player.Stat.physicalDamageInflict.GetValue());

        playerAttack.SetCurrentAttackInfo(attackInfo);

        player.MovementCompo.StopImmediately();

        //�ִϸ��̼� ���� �޺�ī���ͷ� �׸��� +1 �ٵ� 2�̻��̸� �ٽ� 0����
        player.animatorCompo.SetInteger(comboCounterHash, player.ComboCounter++);
        if (player.ComboCounter > 1) player.ComboCounter = 0;

        player.lastAttackTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(IsTriggerCalled(AnimationTriggerEnum.AttackTrigger))
        {
            playerAttack.Attack();
        }
        if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {

            if (player.IsGroundDetected())
                stateMachine.ChangeState(PlayerStateEnum.Idle);
            else
                stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
}
