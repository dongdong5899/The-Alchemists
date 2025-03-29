using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Boom : MonoBehaviour
{
    private float _radius, _delay, _lastBoomTime;
    private int _damage, _repeat, _repeatCnt = 0;

    private Entity _owner;
    private Collider2D[] _colliders = new Collider2D[5];
    private List<Collider2D> _hitedColls;

    public void Init(float radius, int damage, float delay, int repeat, Entity owner)
    {
        _radius = radius;
        _damage = damage;
        _delay = delay;
        _repeat = repeat;
        _owner = owner;

        _hitedColls = new List<Collider2D>();
    }

    private void Update()
    {
        if (_repeatCnt < _repeat)
        {
            if (_lastBoomTime + _delay < Time.time)
            {
                _lastBoomTime = Time.time;
                _repeatCnt++;
                ApplyBoom();
            }
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void ApplyBoom()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _colliders, 1 << LayerMask.NameToLayer("Enemy"));

        if (_hitedColls.Count <= 0)
            _hitedColls = _colliders.ToList();
        else
        {
            Debug.Log(_hitedColls.Count());
            Debug.Log(_colliders.Count());
            _hitedColls = _hitedColls.Intersect(_colliders.ToList()).ToList();
        }


        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].TryGetComponent(out Health health))
            {
                Vector2 dir = (transform.position - health.transform.position).normalized;
                health.TakeDamage(_damage, dir * -4.5f, _owner);
            }
        }
        ParticleSystem effect = Instantiate(EffectInstantiateManager.Instance.boomEffect, transform.position, Quaternion.identity);
        effect.transform.localScale = Vector3.one * _radius / 3;

        if (_repeatCnt == 3)
        {
            if (_hitedColls.Count == 0) return;
            _hitedColls.ForEach(coll =>
            {
                if (coll.TryGetComponent<Entity>(out Entity entity))
                {
                    Debug.Log("Stun");
                    entity.Stun(1);
                }
            });
        }
    }
}
