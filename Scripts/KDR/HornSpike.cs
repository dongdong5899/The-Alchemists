using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornSpike : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private float _damageDelay;
    private float _lastDamageTime;
    private float _spawnTime;
    private int _damage;
    private float _lifetime;
    private bool _onFragileEffect;

    [Header("Collider")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;

    private Collider2D[] _colliders = new Collider2D[10];
    private Entity _owner;

    public void Init(Entity owner, Vector2 dir, float radius, int damage, float size, float lifetime, bool onFragileEffect)
    {
        _owner = owner;
        _damage = damage;
        _lifetime = lifetime;
        _onFragileEffect = onFragileEffect;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius * size * 0.9f, dir, 1, _whatIsGround);

        _spawnTime = Time.time;

        transform.localScale = Vector2.one * size;
        transform.up = hit.normal;
        transform.position = hit.point + (Vector2)transform.up * 0.5f * size;
    }

    private void Update()
    {
        int cnt = Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + _offset, _size, 0, _colliders, _whatIsTarget);
        if (cnt != 0 && _lastDamageTime + _damageDelay < Time.time)
        {
            _lastDamageTime = Time.time;
            for (int i = 0; i < cnt; i++)
            {
                if (_colliders[i].TryGetComponent(out Entity entity))
                {
                    entity.healthCompo.TakeDamage(_damage, Vector2.zero, _owner);

                    if (_onFragileEffect)
                        entity.ApplyStatusEffect(StatusDebuffEffectEnum.Fragile, 1, 5f);
                }
            }
        }

        if (_spawnTime + _lifetime < Time.time)
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + _offset, _size);
    }
#endif
}
