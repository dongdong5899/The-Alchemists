using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarRushSkill : Skill
{
    private WildBoar wildBoar;

    public override void UseSkill()
    {
        if (wildBoar == null) wildBoar = owner as WildBoar;
        wildBoar.StateMachine.ChangeState(WildBoarEnum.Rush);
    }
}
