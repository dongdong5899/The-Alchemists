using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectaryCircle : MonoBehaviour
{
    private Transform _playerTrm;
    private SpriteRenderer _renderer;
    private Color originColor;

    [SerializeField] private float maxDist;

    private void Start()
    {
        _playerTrm = PlayerManager.Instance.PlayerTrm;
        _renderer = GetComponent<SpriteRenderer>();
        originColor = _renderer.color;
    }

    private void Update()
    {
        //float dist = (_playerTrm.position - transform.position).magnitude;
        //float alpha = Mathf.Lerp(0, 255, maxDist / dist);

        //_renderer.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
    }
}
