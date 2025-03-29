using UnityEngine;

public interface IDamageable
{
    public bool TakeDamage(int damage, Vector2 knockPower, Entity dealer, bool isPersent = false);
}