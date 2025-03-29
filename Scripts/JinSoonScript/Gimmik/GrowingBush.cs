using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrowingBush : MonoBehaviour, IAffectable
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private Coroutine _growingCoroutine;

    [SerializeField]
    private Sprite _1x1Sprite;
    [SerializeField]
    private Sprite _3x3Sprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.mass = 3000;
    }


    public void ApplyEffect()
    {
        if (_growingCoroutine == null)
            _growingCoroutine = StartCoroutine(GrowingCoroutine());
    }

    private IEnumerator GrowingCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("Ground");
        _spriteRenderer.sprite = _3x3Sprite;
        _collider.size = Vector2.one * 3;
        yield return new WaitForSeconds(5f);
        gameObject.layer = LayerMask.NameToLayer("Push");
        _spriteRenderer.sprite = _1x1Sprite;
        _collider.size = Vector2.one;

        _growingCoroutine = null;
    }
}
