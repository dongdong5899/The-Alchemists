using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationTriggerable
{
    public void AnimationTrigger(AnimationTriggerEnum trigger);
    public bool IsTriggered(AnimationTriggerEnum trigger);
    public void RemoveTrigger(AnimationTriggerEnum trigger);
}
