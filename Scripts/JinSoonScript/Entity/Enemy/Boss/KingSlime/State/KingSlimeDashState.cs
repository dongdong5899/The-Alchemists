using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeDashState : EnemyState<KingSlimeStateEnum>
{
    private KingSlime _kingSlime;
    private BoxCollider2D _kingSlimeCollider;
    private Vector2 _dashDir;

    private bool _isDashStarted = false;

    private readonly int _dashStartTrigger = Animator.StringToHash("DashStart");
    private readonly int _dashReadyTrigger = Animator.StringToHash("DashReady");

    public KingSlimeDashState(Enemy<KingSlimeStateEnum> enemy, EnemyStateMachine<KingSlimeStateEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _kingSlime = enemy as KingSlime;
        _kingSlimeCollider = enemy.colliderCompo as BoxCollider2D;
    }

    public override void Enter()
    {
        base.Enter();
        _kingSlime.StartCoroutine(DashCoroutine());
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_isDashStarted)
        {
            if (_kingSlime.IsWallDetected(out Collider2D collider)) // �Ĺھҵ�? �� �� �ĹھҴ����� �Ⱦ˷�����? �� �˰�ʹ�!!!!! ��� �����ε�
            {
                if(collider.TryGetComponent(out GrowingBush bush))
                {
                    _kingSlime.SetCanBeStun(true);
                    _kingSlime.Stun(2);
                    _kingSlime.SetCanBeStun(false);
                    GameObject.Destroy(bush.gameObject);
                }
                else
                {
                    _kingSlime.FlipController(-_dashDir.x);
                    enemyStateMachine.ChangeState(KingSlimeStateEnum.Idle);
                }
                CameraManager.Instance.ShakeCam(5, 1, 0.5f);
                _kingSlime.contactHit.enabled = false;
                _kingSlime.KnockBack(-_dashDir * 3f);
            }
        }
    }

    public override void Exit()
    {
        _isDashStarted = false;
        base.Exit();
    }

    private IEnumerator DashCoroutine()
    {
        //���� �� _kingSlime.dashInfos �ϳ� �̾ƿͼ� ���� �����̶� �̰����� �� �����ָ� ��.
        //��ġ�� �ٲ��ְ�.
        //DOJump��� �� ��� �޼��尡 �־��ݾ�???
        DashInfo info = _kingSlime.dashInfos[Random.Range(0, _kingSlime.dashInfos.Length)];
        _dashDir = info.direction;
        _kingSlime.FlipController(-_dashDir.x);
        _kingSlime.transform.DOJump(info.dashStartPos.position, 5, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _kingSlime.contactHit.enabled = true;
        _kingSlime.animatorCompo.SetTrigger(_dashReadyTrigger);
        _kingSlime.FlipController(_dashDir.x);
        _kingSlime.patternEffects[0].gameObject.SetActive(true);
        Vector2 effectStartPos = (Vector2)_kingSlime.transform.position + -_dashDir * (_kingSlimeCollider.size.x * 0.7f);
        _kingSlime.patternEffects[0].SetPatternVisual(new Vector2(32, _kingSlimeCollider.size.y + 2),
            effectStartPos,
            effectStartPos + _dashDir * 32,
            _kingSlime.beforeDashDelay * 0.7f,
            new Vector2(0, _kingSlimeCollider.size.y + 2));
        yield return new WaitForSeconds(_kingSlime.beforeDashDelay);
        _kingSlime.animatorCompo.SetTrigger(_dashStartTrigger);
        _kingSlime.patternEffects[0].gameObject.SetActive(false);
        _kingSlime.MovementCompo.SetVelocity(_dashDir * _kingSlime.dashSpeed);
        _isDashStarted = true;
    }
}
