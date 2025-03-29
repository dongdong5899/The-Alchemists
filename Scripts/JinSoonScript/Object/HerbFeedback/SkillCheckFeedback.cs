using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckFeedback : HerbGatheringFeedback
{
    [SerializeField] private SkillCheck skillCheck;
    private Herbs curHerb;

    public override void PlayFeedback(Herbs herb)
    {
        if (isPlayed) return;

        skillCheck.Init();
        curHerb = herb;
        isPlayed = true;
        skillCheck.StartSkillCheck();
    }

    private void Update()
    {
        if (isPlayed && skillCheck.IsEnd)
        {
            //성공함
            if (skillCheck.Result == true)
            {

            }
            else    //실패함
            {
                curHerb.ReduceTime(1f);
            }

            isPlayed = false;
        }
    }
}
