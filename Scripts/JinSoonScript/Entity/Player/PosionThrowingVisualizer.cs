using UnityEngine;

public class PosionThrowingVisualizer : MonoBehaviour
{
    private InputReader _inputReader;

    [SerializeField] private GameObject _aimNothing;
    [SerializeField] private GameObject _aimEnemy;

    [Space(10)]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private float _correction;

    [Space(10)]
    private float _throwingSpeed = 15f;
    [SerializeField] private float _gravityScale = -9.8f;

    private Player _player;
    private Transform _playerTrm;
    private Collider2D[] _coll;
    private RaycastHit2D[] _raycast;
    private Vector2 _throwingDir;

    private void Awake()
    {
        _inputReader = PlayerManager.Instance.Player.PlayerInput;
        _player = PlayerManager.Instance.Player;
        _playerTrm = PlayerManager.Instance.PlayerTrm;
        _coll = new Collider2D[1];
        _raycast = new RaycastHit2D[1];
    }

    private void Update()
    {
        CheckMousePosition();
    }

    private void CheckMousePosition()
    {
        Vector2 mouseScreenPosition = _inputReader.MouseScreenPosition;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        int detectedEnemy = Physics2D.OverlapCircleNonAlloc(mousePos, _correction, _coll, _whatIsEnemy);
        if (detectedEnemy > 0)
        {
            transform.position = _coll[0].transform.position + (Vector3)_coll[0].offset;
            transform.localScale = _coll[0].bounds.size * 1.2f;
            if (!_aimEnemy.activeSelf) _aimEnemy.SetActive(true);
            if (_aimNothing.activeSelf) _aimNothing.SetActive(false);

            Vector2 direction = CalculateThrowDirection(_playerTrm.position, _coll[0].transform.position);
            _player.PortionThrowingDir = (direction);

            return;
        }


        int detectedGround = Physics2D.BoxCastNonAlloc
            (mousePos, Vector2.one * _correction, 0, Vector2.zero, _raycast, _whatIsGround);

        if (_aimEnemy.activeSelf) _aimEnemy.SetActive(false);
        if (!_aimNothing.activeSelf) _aimNothing.SetActive(true);
        transform.position = mousePos;
        transform.localScale = Vector3.one;

        Vector2 dir = CalculateThrowDirection(_playerTrm.position + _player.ThrowingOffset, mousePos);
        _player.PortionThrowingDir = (dir);
    }


    private Vector2 CalculateThrowDirection(Vector2 startPosition, Vector2 targetPosition)
    {
        Vector2 targetDir = targetPosition - startPosition;
        _throwingSpeed = targetDir.magnitude * Mathf.Abs(_gravityScale) / 9.8f;

        float dx = targetDir.x;
        float dy = targetDir.y;

        float time = Mathf.Abs(dx) / _throwingSpeed;

        float vx = dx / time;
        float vy = (dy + .5f * -_gravityScale * Mathf.Pow(time, 2)) / time;

        _throwingDir = new Vector2(vx, vy).normalized;
        return _throwingDir * _throwingSpeed;
    }
}
