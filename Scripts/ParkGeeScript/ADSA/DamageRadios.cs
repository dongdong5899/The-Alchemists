using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRadios : MonoBehaviour
{
    private float _destroyTime = 1f;
    private Entity targetEntity;
    private float _damageInterval = 0.5f; 
    private float _collisionRadius = 0.5f;

    private void Start()
    {
        StartCoroutine(DestroyCoroutine());
        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

    public void Initialize(Entity target)
    {
        targetEntity = target;
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_damageInterval);
            if (targetEntity != null && IsCollidingWithTarget())
            {
                targetEntity.healthCompo.TakeDamage(10, Vector2.zero, null);
                Debug.Log("´ë¹ÌÁö");
                break;
            }
        }
    }

    private bool IsCollidingWithTarget()
    {
        return Vector3.Distance(transform.position, targetEntity.transform.position) <= _collisionRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _collisionRadius);
    }
}