using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum VineState
{
    Growing,
    Grown,
    Shrinking,
    Shrunk,
}

public class GrowingGrass : MonoBehaviour, IAffectable, IAnimationTriggerable
{
    [SerializeField]
    private Animator _animator;

    private Coroutine _growCoroutine;

    private readonly int _growStartHash = Animator.StringToHash("GrowStart");
    private readonly int _growResetHash = Animator.StringToHash("GrowReset");

    private int _animationTriggerBit = 0;

    public VineState CurrentState { get; private set; } = VineState.Grown;

    private void Awake()
    {
        CurrentState = VineState.Shrunk;
    }

    public void ApplyEffect()
    {
        if (_growCoroutine == null)
            _growCoroutine = StartCoroutine(IEGrowing());
    }

    private IEnumerator IEGrowing()
    {
        CurrentState = VineState.Growing;
        _animator.SetTrigger(_growStartHash);
        yield return new WaitUntil(() => IsTriggered(AnimationTriggerEnum.EndTrigger));
        CurrentState = VineState.Grown;
        yield return new WaitForSeconds(5f);
        CurrentState = VineState.Shrinking;
        _animator.SetTrigger(_growResetHash);
        yield return new WaitUntil(() => IsTriggered(AnimationTriggerEnum.EndTrigger));

        CurrentState = VineState.Shrunk;

        _growCoroutine = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.StateMachine.CurrentState.GetType() == typeof(PlayerClimbState))
                player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public void AnimationTrigger(AnimationTriggerEnum trigger)
    {
        _animationTriggerBit |= (int)trigger;
    }

    public bool IsTriggered(AnimationTriggerEnum trigger)
    {
        bool isTriggred = (_animationTriggerBit & (int)trigger) != 0;
        if (isTriggred)
            RemoveTrigger(trigger);
        return isTriggred;
    }

    public void RemoveTrigger(AnimationTriggerEnum trigger)
    {
        _animationTriggerBit &= ~(int)trigger;
    }
}
