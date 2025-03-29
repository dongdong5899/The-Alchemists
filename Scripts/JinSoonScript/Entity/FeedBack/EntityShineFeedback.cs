using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityShineFeedback : FeedBack
{
    [SerializeField] private EntityVisual _visual;

    public override void Play()
    {
        _visual.Hit();
    }
}
