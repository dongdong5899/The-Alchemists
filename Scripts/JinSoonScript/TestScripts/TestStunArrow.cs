using UnityEngine;

public class TestStunArrow : MonoBehaviour
{
    [SerializeField] private float stunTime = 1.5f;

    private void Update()
    {
        transform.position += Vector3.left * 3f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.TryGetComponent<Player>(out Player p))
        {
            Vector2 stunPower = new Vector2((p.transform.position - transform.position).normalized.x, 1f) * 5;
            p.healthCompo.TakeDamage(2, stunPower, new Player());
            p.Stun(stunTime);
            Destroy(gameObject);
        }
    }
}
