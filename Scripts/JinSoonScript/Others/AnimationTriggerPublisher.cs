using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerPublisher : MonoBehaviour
{
    [SerializeField]
    private IAnimationTriggerable _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<IAnimationTriggerable>();
    }

    public void AnimationTriggerByEnum(AnimationTriggerEnum trigger)
    {
        _owner.AnimationTrigger(trigger);
    }
}