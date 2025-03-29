using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private GameObject _rockParticle;
    private void OnEnable()
    {
        float size = Random.Range(1.5f, 2.5f);
        transform.localScale = Vector3.one * size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable health))
            health.TakeDamage(1, Vector2.zero, null);

        //파티클 나오게하고
        Instantiate(_rockParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
