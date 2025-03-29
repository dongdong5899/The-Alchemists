using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatAnimationTrigger : MonoBehaviour
{
    private Goat goat;

    private void Awake()
    {
        goat = GetComponentInParent<Goat>();
    }

    public void AnimationFinishTrigger()
    {
        goat.AnimationFinishTrigger();
    }
}
