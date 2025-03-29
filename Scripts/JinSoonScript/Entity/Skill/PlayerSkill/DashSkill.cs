using UnityEngine;

public class DashSkill : Skill
{
    private float dashCoolTime;
    private float dashStartTime;

    public float dashPower;
    public float dashTime;

    public bool canUseSkill;
    public bool isInvincibleWhileDash;
    public bool isAttackWhileDash;

    public override void UseSkill()
    {
        //대쉬 쿨타임이면 return;
        if (dashStartTime + dashCoolTime > Time.time) return;
        //스킬이 해금되지 않았으면 return
        Player player = owner as Player;
        if (player.canDash == false) return;

        dashStartTime = Time.time;

        player.Dash(dashTime / 10f, dashPower, isInvincibleWhileDash, isAttackWhileDash);
    }

    public void Init(float dashPower, float dashTime, bool canUseSkill, bool isInvincibleWhileDash, bool isAttackWhileDash, float dashCoolTime)
    {
        this.dashPower = dashPower;
        this.dashTime = dashTime;
        this.canUseSkill = canUseSkill;
        this.isInvincibleWhileDash = isInvincibleWhileDash;
        this.isAttackWhileDash = isAttackWhileDash;
        this.dashCoolTime = dashCoolTime;
    }
}
