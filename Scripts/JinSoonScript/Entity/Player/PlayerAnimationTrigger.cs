using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationTriggerEnum
{
    EndTrigger = 1,
    AttackTrigger = 2,
    EventTrigger = 4,
}

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    public void AnimationTrigger(AnimationTriggerEnum trigger)
    {
        player.AnimationTrigger(trigger);
    }
}
