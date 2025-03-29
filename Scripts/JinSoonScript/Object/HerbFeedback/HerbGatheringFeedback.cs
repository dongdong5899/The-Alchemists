using UnityEngine;

public abstract class HerbGatheringFeedback : MonoBehaviour
{
    //���� �� �ǵ���� �������ٰ���
    [SerializeField] protected float playFeedbackTime;
    protected bool isPlayed = false;

    public float PlayFeedbackTime => playFeedbackTime;

    public abstract void PlayFeedback(Herbs herb);
}
