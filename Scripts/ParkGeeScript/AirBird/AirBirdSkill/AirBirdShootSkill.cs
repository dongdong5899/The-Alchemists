using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdShootSkill : Skill
{
    private AirBird airBird;

    public override void UseSkill()
    {
        if (airBird == null) airBird = owner as AirBird;
        airBird.StateMachine.ChangeState(AirBirdEnum.Shoot);
    }
}
