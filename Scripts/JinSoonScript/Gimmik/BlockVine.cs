using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVine : MonoBehaviour, IAffectable
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _disappearTime = 3f;
    private Coroutine _disappearCoroutine;
    public void ApplyEffect()
    {
        if (_disappearCoroutine != null) return;
        _disappearCoroutine = StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator DisappearCoroutine()
    {
        float percent = 0;
        while (percent < 1f)
        {
            percent += Time.deltaTime * (1f * _disappearTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
