using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdAnimationTrigger : MonoBehaviour
{
    private AirBird airbird;

    private void Awake()
    {
        airbird = GetComponentInParent<AirBird>();
    }

    public void AnimationFinishTrigger()
    {
        airbird.AnimationFinishTrigger();
    }
}
