using System.Collections;
using UnityEngine;

public class TestWildBoar : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private GameObject _checkPlayer;
    private float _distance = 5f;
    private float _dashtime = 1.5f;
    private float _minDashSpeed = 5f;
    private float _maxDashSpeed = 20f;
    private float _dashDelay = 3f;
    private bool isDashing = false;
    private bool isCoroutineRunning = false;
    private Vector3 dashDirection;
    private float dashStartTime;

    private void Awake()
    {
        _player = PlayerManager.Instance.PlayerTrm;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_player.position, transform.position);

        if (distanceToPlayer < _distance && !isDashing && !isCoroutineRunning)
        {
            StartCoroutine(DelayDash());
        }

        if (isDashing)
        {
            float elapsedTime = Time.time - dashStartTime;
            float currentSpeed = Mathf.Lerp(_minDashSpeed, _maxDashSpeed, elapsedTime / _dashtime);
            //Debug.Log(currentSpeed);
            float distanceToMove = currentSpeed * Time.deltaTime;
            transform.position += dashDirection * distanceToMove;

            if (elapsedTime >= _dashtime)
            {
                isDashing = false;
                _checkPlayer.SetActive(false);
            }
        }
    }

    private IEnumerator DelayDash()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(_dashDelay - 0.5f);
        _checkPlayer.SetActive(true);
        yield return new WaitForSeconds(1f);

        dashDirection = (_player.position - transform.position).normalized;
        dashDirection = new Vector3(dashDirection.x, 0, dashDirection.z);
        dashStartTime = Time.time;
        isDashing = true;
        isCoroutineRunning = false;
    }
}
