using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedBackPlayer : MonoBehaviour
{
    private List<FeedBack> _feedbacks;

    private void Awake()
    {
        _feedbacks = GetComponents<FeedBack>().ToList();
    }

    public void PlayFeedBacks()
    {
        _feedbacks.ForEach(feedback => feedback.Play());
    }
}
