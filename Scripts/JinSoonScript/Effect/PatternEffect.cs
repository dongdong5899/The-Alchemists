using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternEffect : MonoBehaviour
{
    private Collider2D[] _colliders;
    [SerializeField]
    private LayerMask _whatIsPlayer;

    private void Awake()
    {
        _colliders = new Collider2D[1];
    }

    public void SetPatternVisual(Vector2 size, Vector2 start, Vector2 end, float duration, Vector2 startSize)
    {
        transform.localScale = startSize;
        StartCoroutine(SetPatternVisualCoroutine(size, start, end, duration, startSize));
    }

    private IEnumerator SetPatternVisualCoroutine(Vector2 size, Vector2 start, Vector2 end, float duration, Vector2 startSize)
    {
        float percent = 0;
        while (percent < 1)
        {
            transform.position = Vector2.Lerp(start, end, percent * 0.5f);
            transform.localScale = Vector2.Lerp(startSize, size, percent);
            percent += Time.deltaTime * (1 / duration);
            yield return null;
        }
    }

    public void DamageCast(int damage, Vector2 knockback)
    {
        Physics2D.OverlapBoxNonAlloc(transform.position, transform.localScale, 0, _colliders, _whatIsPlayer);
        if (_colliders[0] != null)
        {
            if (_colliders[0].TryGetComponent(out IDamageable health))
            {
                health.TakeDamage(damage, knockback, null);
            }
        }
    }
}
