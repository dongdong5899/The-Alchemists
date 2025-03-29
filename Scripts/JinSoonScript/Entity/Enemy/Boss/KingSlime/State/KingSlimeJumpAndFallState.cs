using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeJumpAndFallState : EnemyState<KingSlimeStateEnum>
{
    private KingSlime _kingSlime;
    private BoxCollider2D _kingSlimeCollider;

    private readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
    private readonly int _fallTriggerHash = Animator.StringToHash("Fall");

    private GrowingBush _bush;

    public KingSlimeJumpAndFallState(Enemy<KingSlimeStateEnum> enemy, EnemyStateMachine<KingSlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _kingSlime = enemy as KingSlime;
        _kingSlimeCollider = _kingSlime.colliderCompo as BoxCollider2D;
    }

    public override void Enter()
    {
        base.Enter();
        _kingSlime.contactHit.enabled = true;
        _kingSlime.StartCoroutine(JumpAndFallCorotuine());
    }

    public override void Exit()
    {
        _kingSlime.contactHit.enabled = false;
        base.Exit();
    }

    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(_kingSlime.beforeJumpDelay);
        _kingSlime.animatorCompo.SetTrigger(_jumpTriggerHash);
        _kingSlime.transform.DOMoveY(_kingSlime.transform.position.y + _kingSlime.jumpYDistance, 0.6f);
        _kingSlime.SetGravityActive(false);
        yield return new WaitForSeconds(0.6f);
    }

    private IEnumerator JumpAndFallCorotuine()
    {
        yield return JumpCoroutine();
        _kingSlime.transform.position = new Vector3(_kingSlime.centerTrm.position.x, _kingSlime.transform.position.y);
        _kingSlime.animatorCompo.SetTrigger(_fallTriggerHash);
        _kingSlime.patternEffects[0].gameObject.SetActive(true);
        _kingSlime.patternEffects[1].gameObject.SetActive(true);

        _kingSlime.patternEffects[0].SetPatternVisual(
            new Vector2(_kingSlimeCollider.size.x, _kingSlime.jumpYDistance), 
            _kingSlime.transform.position, 
            _kingSlime.centerTrm.position - new Vector3(0, 0.5f), 
            2f, 
            new Vector2(_kingSlimeCollider.size.x, 0));

        _kingSlime.patternEffects[1].SetPatternVisual(
            new Vector2(32, 0.5f), 
            _kingSlime.centerTrm.position, 
            _kingSlime.centerTrm.position + new Vector3(0, 0.5f), 
            1f, 
            new Vector2(32, 0));

        yield return new WaitForSeconds(_kingSlime.beforeFallDelay);
        _kingSlime.SetGravityActive(true);
        _kingSlime.MovementCompo.SetVelocity(new Vector2(0, -60), false, true);
        yield return new WaitUntil(() => _kingSlime.IsGroundDetected(new Vector2(0, -1f)));
        _kingSlime.patternEffects[1].DamageCast(5, Vector3.up * 5f);
        _kingSlime.patternEffects[0].gameObject.SetActive(false);
        _kingSlime.patternEffects[1].gameObject.SetActive(false);
        CameraManager.Instance.ShakeCam(5, 1, 0.5f);
        if (_bush == null)
            SpawnGrowingBush();
        enemyStateMachine.ChangeState(KingSlimeStateEnum.Idle);
    }

    private void SpawnGrowingBush()
    {
        _bush = GameObject.Instantiate(_kingSlime.bushPrefab, (Vector2)_kingSlime.centerTrm.position + new Vector2(Random.Range(-2f, 2f), _kingSlime.jumpYDistance), Quaternion.identity);
    }
}
