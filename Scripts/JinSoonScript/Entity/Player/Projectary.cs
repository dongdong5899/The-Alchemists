using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Projectary : MonoBehaviour
{
    [SerializeField]
    private float _time;
    [SerializeField]
    private int _count;
    [SerializeField]
    private float _gravity;

    [SerializeField]
    private GameObject _projectaryPrefab;

    private List<Transform> _projectileList = new List<Transform>();

    [SerializeField]
    private LayerMask _whatIsObstacle;
    private float _delta = 0;

    private void Awake()
    {
        SetData(_time, _count);
    }

    public void SetData(float time, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(_projectaryPrefab, transform);
            _projectileList.Add(g.transform);
            g.SetActive(false);
        }
        _delta = _time / _count;
    }

    public void DrawLine(Vector2 pos, Vector3 power)
    {
        bool flag = true;
        for (int i = 0; i < _projectileList.Count; i++)
        {
            Transform t = _projectileList[i];
            if (flag)
            {
                t.gameObject.SetActive(true);

                Vector2 dotPos;
                float time = _delta * i;
                dotPos.x = pos.x + power.x * time;
                dotPos.y = pos.y + power.y * time + (_gravity * Mathf.Pow(time, 2)) * 0.5f;

                if (Physics2D.OverlapCircle(dotPos, .3f, _whatIsObstacle))
                {
                    flag = false;
                }

                t.position = dotPos;
            }
            else
                t.gameObject.SetActive(false);
        }
    }

    public void DisableProjectile()
    {
        for (int i = 0; i < _projectileList.Count; i++)
        {
            _projectileList[i].gameObject.SetActive(false);
        }
    }
}
