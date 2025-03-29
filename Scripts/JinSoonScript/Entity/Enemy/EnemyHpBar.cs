using System.Collections;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Transform _fill;
    private Transform _parent;
    private Vector3 origin;

    private Coroutine _hpDownRoutine;

    private void Awake()
    {
        _parent = transform.parent;
        origin = transform.localScale;
    }

    private void Update()
    {
        if (_parent.eulerAngles.y <= -180)
        {
            transform.localScale = new Vector3(-origin.x, origin.y, origin.z);
        }
        else
        {
            transform.localScale = origin;
        }
    }

    public void SetHp(float maxHp, float curHp)
    {
        float percentage = curHp / maxHp;

        if (_hpDownRoutine != null)
            StopCoroutine(_hpDownRoutine);

        _hpDownRoutine = StartCoroutine(HpDownRoutine(percentage));
        //_pivot.localScale = new Vector3(percentage, 1, 1);
    }

    private IEnumerator HpDownRoutine(float percentage)
    {
        float process = 1;
        float origin = _fill.localScale.x;

        while (process > 0)
        {
            float xScale = Mathf.Lerp(percentage, origin, process);
            _fill.localScale = new Vector3(xScale, 1, 1);
            process -= Time.deltaTime * 5;
            yield return null;
        }
    }
}
