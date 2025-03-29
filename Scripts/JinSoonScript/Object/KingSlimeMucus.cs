using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeMucus : MonoBehaviour
{
    [SerializeField] private SlowFlooring _slowFlooring;
    private Rigidbody2D rigid;
    private Entity _owner;
    private float _slowFieldEnableTime = 3f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable health))
        {
            health.TakeDamage(1, Vector2.zero, _owner);
        }

        SlowFlooring s = Instantiate(_slowFlooring).GetComponent<SlowFlooring>();
        s.transform.position = transform.position;
        s.Init(_slowFieldEnableTime);

        Destroy(gameObject);
    }

    public void Fire(Vector2 direction, Entity owner, float slowFieldEnableTime)
    {
        _slowFieldEnableTime = slowFieldEnableTime;
        rigid.AddForce(direction, ForceMode2D.Impulse);
    }
}
