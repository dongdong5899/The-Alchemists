using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _rockPf;
    [SerializeField] private float _minX, _maxX;

    private bool _isFallingRock = false;
    private float _endTime;

    private float _rockSpawnDelayMin = 0.10f;
    private float _rockSpawnDelayMax = 0.32f;
    private float _rockSpawnDelay;

    private void Update()
    {
        if (_isFallingRock == false || Time.time < _rockSpawnDelay) return;

        _rockSpawnDelay = Time.time + Random.Range(_rockSpawnDelayMin, _rockSpawnDelayMax);

        Transform rockTrm = Instantiate(_rockPf, transform).transform;
        float x = Random.Range(_minX, _maxX);
        rockTrm.position = new Vector2(x, transform.position.y);

        if (Time.time >= _endTime)
            _isFallingRock = false;
    }

    public void StartRockFalling(int time)
    {
        _endTime = Time.time + time;
        _isFallingRock = true;
    }
}
