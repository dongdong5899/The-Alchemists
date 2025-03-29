using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBirdShootState : EnemyState<AirBirdEnum>
{
    private AirBird _airBird;
    private Transform _playerTrm;

    private float[] _offset = { -20f, 0f, 20f };

    public AirBirdShootState(Enemy<AirBirdEnum> enemy, EnemyStateMachine<AirBirdEnum> enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _playerTrm = PlayerManager.Instance.PlayerTrm;
        _airBird = enemy as AirBird;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.MovementCompo.StopImmediately(true);
    }

    public override void AnimationFinishTrigger()
    {
        if (triggerCall)
        {
            enemyStateMachine.ChangeState(AirBirdEnum.Idle);
            return;
        }

        AudioManager.Instance.PlaySound(SoundEnum.BirdAttack, enemy.transform);

        Vector2 defaultShootMovement = (_playerTrm.position + Vector3.up - enemy.transform.position).normalized * _airBird.featherShootSpeed;
        for (int i = 0; i < 3; i++)
        {
            Vector2 shootMovement = Quaternion.Euler(0, 0, _offset[i]) * defaultShootMovement;

            Feather feather = GameObject.Instantiate(_airBird.featherPrefab, enemy.transform.position, Quaternion.identity);
            feather.Shoot(shootMovement);
        }

        base.AnimationFinishTrigger();
    }
}
