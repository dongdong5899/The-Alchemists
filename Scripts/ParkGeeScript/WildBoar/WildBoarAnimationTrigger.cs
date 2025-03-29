using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarAnimationTrigger : MonoBehaviour
{
    private WildBoar wildBoar;

    private void Awake()
    {
        wildBoar = GetComponentInParent<WildBoar>();
    }

    public void AnimationFinishTrigger()
    {
        wildBoar.AnimationFinishTrigger();
    }
}
