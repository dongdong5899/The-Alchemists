using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarFeedback : FeedBack
{
    [SerializeField] private EnemyHpBar _hpBar;
    [SerializeField] private Health _enemyHealth;

    public override void Play()
    {
        _hpBar.SetHp(_enemyHealth.maxHp.GetValue(), (float)_enemyHealth.curHp);
    }
}
