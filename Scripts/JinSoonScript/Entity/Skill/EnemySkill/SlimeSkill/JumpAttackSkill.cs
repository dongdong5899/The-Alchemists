using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackSkill : Skill
{
    private Slime slime;

    public override void UseSkill()
    {
        if (slime == null) slime = owner as Slime;
        slime.StateMachine.ChangeState(SlimeStateEnum.JumpAttack);
    }
}
