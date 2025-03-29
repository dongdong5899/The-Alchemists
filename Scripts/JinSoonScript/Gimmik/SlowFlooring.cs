using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SlowFlooring : Probs
{
    [SerializeField] private float slowDownValue = 5f;
    [SerializeField] private float _offset = 0.1f;
    [SerializeField] private LayerMask _whatIsGround;

    private SpriteRenderer _renderer;
    private bool _isSlowDown = false;
    private bool _isInteractedThisFrame = false;
    private Player _player;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _player = PlayerManager.Instance.Player;
    }

    public void Init(float enableTime)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, _whatIsGround);

        if (hit.collider != null)
            transform.position = new Vector2(transform.position.x, transform.position.y - hit.distance - _offset);

        StartCoroutine(DelayDisable(enableTime));
    }

    public override void Interact(Entity entity)
    {
        if (_isSlowDown == false)
        {
            _player.Stat.moveSpeed.AddModifier(-slowDownValue);
            _isSlowDown = true;
        }
        _isInteractedThisFrame = true;
    }

    private void LateUpdate()
    {
        if (!_isInteractedThisFrame && _isSlowDown)
        {
            _player.Stat.moveSpeed.RemoveModifier(-slowDownValue);
            _isSlowDown = false;
        }
        _isInteractedThisFrame = false;
    }

    private IEnumerator DelayDisable(float time)
    {
        yield return new WaitForSeconds(time);
        _renderer.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                _player.Stat.moveSpeed.RemoveModifier(-slowDownValue);
                _isSlowDown = false;
                Destroy(gameObject);
            });
    }
}
