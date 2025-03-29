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
            //������
            if (skillCheck.Result == true)
            {

            }
            else    //������
            {
                curHerb.ReduceTime(1f);
            }

            isPlayed = false;
        }
    }
}
