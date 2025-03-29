using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private GameObject _interact;
    [SerializeField] private float _detectionRadius = 5f;

    private bool _isPlayerInRange = false;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = PlayerManager.Instance.PlayerTrm;
    }

    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        if (distance <= _detectionRadius)
        {
            if (!_isPlayerInRange)
            {
                _isPlayerInRange = true;
                OnPlayerEnter();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                UIManager.Instance.Open(UIType.Shop);
            }
        }
        else
        {
            if (_isPlayerInRange)
            {
                _isPlayerInRange = false;
                OnPlayerExit();
            }
        }
    }

    private void OnPlayerEnter()
    {
        _interact.SetActive(true);
        //UIManager.Instance.GuideOn();
    }

    private void OnPlayerExit()
    {
        _interact.SetActive(false);
        //UIManager.Instance.GuideOff();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _detectionRadius);
    }
}
