using UnityEngine;

public class HerbGatheringFeedbackPlayer : MonoBehaviour
{
    private HerbGatheringFeedback[] feedbacks;
    private Herbs herb;

    private void Awake()
    {
        feedbacks = GetComponents<HerbGatheringFeedback>();
        herb = transform.parent.GetComponent<Herbs>();
    }

    public void PlayFeedback(float time)
    {
        foreach (var feedback in feedbacks)
        {
            if (feedback.PlayFeedbackTime - time <= 0f)
                feedback.PlayFeedback(herb);
        }
    }
}
