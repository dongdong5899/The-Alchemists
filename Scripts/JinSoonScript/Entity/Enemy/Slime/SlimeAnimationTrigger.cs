using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationTrigger : MonoBehaviour
{
    private Slime slime;

    private void Awake()
    {
        slime = transform.parent.GetComponent<Slime>();
    }

    public void AnimationFinishTrigger()
    {
        slime.AnimationFinishTrigger();
    }

    public void MoveStart()
    {
        slime.moveAnim = true;
    }

    public void MoveStop()
    {
        slime.moveAnim = false;
    }
}
