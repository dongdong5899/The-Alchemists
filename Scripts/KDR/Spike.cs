using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private CircleCollider2D _circle;
    private Entity _owner;
    private int _damage;
    private float _lifetime = 15f;
    private float _startTime;
    [SerializeField] private LayerMask _whatIsObstacle;
    private RaycastHit2D[] _hit = new RaycastHit2D[1];


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _circle = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.CircleCastNonAlloc(
            transform.position + (Vector3)_circle.offset,
            _circle.radius, _rigid.velocity,
            _hit, 0.1f, _whatIsObstacle) != 0)
        {
            AudioManager.Instance.PlaySound(SoundEnum.SpikeReflect, transform);
            Vector2 reflect = Vector2.Reflect(_rigid.velocity, _hit[0].normal) * 0.9f;
            _rigid.velocity = reflect;
        }
    }

    private void Update()
    {
        if (_startTime + _lifetime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 dir, int damage, Entity owner, float speed, float size)
    {
        _owner = owner;
        _startTime = Time.time;
        _damage = damage;
        _rigid.velocity = dir * speed;
        _rigid.AddTorque(Random.Range(-200f, 200f));
        transform.localScale = Vector3.one * size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health) && !(health.owner is Player))
        {
            Instantiate(EffectInstantiateManager.Instance.spikeHitEffect, health.transform.position, Quaternion.identity);

            health.TakeDamage(_damage, Vector3.zero, _owner);
            CameraManager.Instance.ShakeCam(4f, 8f, 0.1f);
        }
    }
}
