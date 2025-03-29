using UnityEngine;

public abstract class HerbGatheringFeedback : MonoBehaviour
{
    //언제 이 피드백을 실행해줄건지
    [SerializeField] protected float playFeedbackTime;
    protected bool isPlayed = false;

    public float PlayFeedbackTime => playFeedbackTime;

    public abstract void PlayFeedback(Herbs herb);
}
