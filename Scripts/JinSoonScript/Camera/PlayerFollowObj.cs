using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowObj : MonoBehaviour
{
    private Transform toFollow;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        toFollow = PlayerManager.Instance.PlayerTrm;
    }

    private void FixedUpdate()
    {
        transform.position = toFollow.position + (Vector3)offset;
    }
}
