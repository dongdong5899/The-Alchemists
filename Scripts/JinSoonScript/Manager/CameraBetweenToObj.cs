using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBetweenToObj : MonoBehaviour
{
    [HideInInspector] public Transform _obj1, _obj2;

    [SerializeField] private Vector2 _maxDist;

    private void Update()
    {
        if (_obj1 == null || _obj2 == null) return;

        Vector2 center = new Vector2(Mathf.Min(_obj1.position.x, _obj2.position.x), Mathf.Min(_obj1.position.y, _obj2.position.y));
        center.x += Mathf.Abs(_obj1.position.x - _obj2.position.x) / 2;
        center.y += Mathf.Abs(_obj1.position.y - _obj2.position.y) / 2;

        if (_obj1.position.x > _obj2.position.x)
            center.x = Mathf.Clamp(center.x, _obj1.position.x - _maxDist.x, int.MaxValue);
        else
            center.x = Mathf.Clamp(center.x, int.MinValue, _obj1.position.x + _maxDist.x);

        if (_obj1.position.y > _obj2.position.y)
            center.y = Mathf.Clamp(center.y, _obj1.position.y - _maxDist.y, int.MaxValue);
        else
            center.y = Mathf.Clamp(center.y, int.MinValue, _obj1.position.y + _maxDist.y);

        transform.position = center;
    }
}
