using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] private Entity _entity;

    [SerializeField] private int _maxDetectingEnemyNum;
    [SerializeField] private float _attackRange;
    [SerializeField] private Vector2 _attackOffset;
    [SerializeField] private LayerMask _whatIsEnemy;

    private Vector2 _knockBackPower;
    private Vector2 _moveDesire;
    private int _damage = 1;
    private RaycastHit2D[] _hits;

    private void Awake()
    {
        _hits = new RaycastHit2D[_maxDetectingEnemyNum];
        _entity = GetComponent<Entity>();
    }

    /// <summary>
    /// Use With Function SetCurrentAttackInfo
    /// </summary>
    public void Attack()
    {
        int detected = Physics2D.CircleCastNonAlloc(transform.position + new Vector3(_attackOffset.x * _entity.FacingDir, _attackOffset.y, 0), _attackRange, Vector2.right * _entity.FacingDir, _hits, 0.001f, _whatIsEnemy);
        bool isCameraShaked = false;

        Vector2 currentMoveDesire = _moveDesire;
        currentMoveDesire.x *= _entity.FacingDir;
        _knockBackPower.x *= _entity.FacingDir;

        _entity.rigidbodyCompo.AddForce(currentMoveDesire, ForceMode2D.Impulse);
        for (int i = 0; i < detected; i++)
        {
            if (_hits[i].transform.TryGetComponent(out IDamageable damageable))
            {
                //Debug.Log(damageable);
                damageable.TakeDamage(_damage, _knockBackPower, _entity);

                Vector3 offset = _knockBackPower.y < 0 ? Vector2.up * 0.2f : Vector2.down * 0.1f;
                Vector3 playerPos = (_entity as Player).PlayerCenter.position + offset;
                Vector2 dir = (_hits[i].transform.position - playerPos).normalized;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;

                //ParticleSystem sliceEffect = Instantiate
                //    (EffectInstantiateManager.Instance.sliceEffect, _hits[i].point, Quaternion.Euler(0, 0, angle));
                //var mainModule = sliceEffect.main;
                //mainModule.startRotation = -angle * Mathf.Deg2Rad;

                if (!isCameraShaked)
                {
                    CameraManager.Instance.ShakeCam(3.5f, 6f, 0.07f);
                    isCameraShaked = true;
                }
            }
        }
    }

    public void SetCurrentAttackInfo(AttackInfo attackInfo)
    {
        _attackRange = attackInfo.radius;
        _attackOffset = attackInfo.offset;
        _knockBackPower = attackInfo.knockBackPower;
        _moveDesire = attackInfo.moveDesire;
        _damage = attackInfo.damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (_entity != null)
            Gizmos.DrawWireSphere(transform.position + new Vector3(_attackOffset.x * _entity.FacingDir, _attackOffset.y, 0), _attackRange);
    }
}
