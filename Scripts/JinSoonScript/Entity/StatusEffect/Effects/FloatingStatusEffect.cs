using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatusEffect : StatusEffect
{
    private int[] damageWithLevel = { 0, 10, 15 };

    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime + 10f);

        Debug.Log("���");
        _target.AirBorn(cooltime);
        _target.StartCoroutine(AirBornDurationCoroutine(cooltime, damageWithLevel[level]));
        _target.animatorCompo.speed = 1;
        _target.CanKnockback = false;
        _target.CanStateChangeable = false;
    }

    IEnumerator AirBornDurationCoroutine(float duration, int damagePercent)
    {
        float elapsedTime = 0.0f;
        float initialVerticalSpeed = 5.0f;

        _target.MovementCompo.StopImmediately(true);
        float prevGravity = _target.MovementCompo.RigidbodyCompo.gravityScale;
        _target.MovementCompo.RigidbodyCompo.gravityScale = 0;

        while (elapsedTime < duration)
        {
            if (elapsedTime < 0.2f)
            {
                float verticalSpeed = initialVerticalSpeed * (1 - elapsedTime / duration);
                _target.MovementCompo.SetVelocity(new Vector2(0, verticalSpeed), withYVelocity: true);
            }
            else
            {
                _target.MovementCompo.StopImmediately(true);
            }


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _target.MovementCompo.RigidbodyCompo.gravityScale = prevGravity;

        float fallSpeed = -40f;
        Debug.Log(_target.MovementCompo.canSetVelocity);
        _target.MovementCompo.SetVelocity(new Vector2(0, fallSpeed), withYVelocity: true);
        yield return new WaitUntil(() =>
        {
            return _target.MovementCompo.RigidbodyCompo.velocity.y > fallSpeed;
        });
        _cooltime = 0;
        _target.CanKnockback = true;
        if (_target.IsUnderStatusEffect(StatusDebuffEffectEnum.Petrification) == false)
        {
            _target.CanStateChangeable = true;
            _target.SetIdle();
        }
        _target.healthCompo.TakeDamage(damagePercent, Vector2.zero, owner, true);
        _target.MovementCompo.StopImmediately(true);

        Debug.Log("��� ����");
    }
}
