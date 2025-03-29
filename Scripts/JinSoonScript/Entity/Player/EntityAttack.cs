using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private int maxDetectingEnemyNum;
    [SerializeField] private float attackRange;
    [SerializeField] private Vector2 attackOffset;
    [SerializeField] private LayerMask whatIsEnemy;

    private Vector2 knockBackPower;
    private int damage = 1;
    private Collider2D[] colls;

    private void Awake()
    {
        colls = new Collider2D[maxDetectingEnemyNum];
        player = GetComponent<Player>();
    }

    /// <summary>
    /// Use With Function SetCurrentAttackInfo
    /// </summary>
    public void Attack()
    {
        int detected = Physics2D.OverlapCircleNonAlloc(transform.position + new Vector3(attackOffset.x * player.FacingDir, attackOffset.y, 0), attackRange, colls, whatIsEnemy);

        for (int i = 0; i < detected; i++)
        {
            if (colls[i].TryGetComponent(out IDamageable health))
            {
                knockBackPower.x *= Mathf.Sign(colls[i].transform.position.x - transform.position.x);
                health.TakeDamage(damage, knockBackPower, player);
            }
        }
    }

    public void SetCurrentAttackInfo(AttackInfo attackInfo)
    {
        attackRange = attackInfo.radius;
        attackOffset = attackInfo.offset;
        knockBackPower = attackInfo.knockBackPower;
        damage = attackInfo.damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(attackOffset.x * player.FacingDir, attackOffset.y, 0), attackRange);
    }
}
