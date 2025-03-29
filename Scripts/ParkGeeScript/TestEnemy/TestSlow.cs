using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlow : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private Player player;

    private bool _isDelay;

    private void Update()
    {
        CheckPlayerInRange();
    }

    private void CheckPlayerInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _distance, _whatIsPlayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                Debug.Log("범위 안에 들어옴");
                SlowAttack();
            }
        }
    }

    private void SlowAttack()
    {
        Debug.Log("공격을 함");
        float xInput = player.PlayerInput.XInput;
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        player.MovementCompo.SetVelocity(new Vector2(xInput * (player.MoveSpeed % 4), playerRigid.velocity.y));
        StartCoroutine(AttackDelay());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
        float xInput = player.PlayerInput.XInput;
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        player.MovementCompo.SetVelocity(new Vector2(xInput * player.MoveSpeed, playerRigid.velocity.y));
        Debug.Log("Attack delay finished");
    }
}
