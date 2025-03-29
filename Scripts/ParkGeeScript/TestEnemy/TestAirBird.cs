//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestAirBird : MonoBehaviour
//{
//    [SerializeField] private GameObject _player;
//    [SerializeField] private GameObject _featherPrefabs;
//    private int _featherCnt = 3;
//    private float _shootCooldown= 5f;
//    private float _chargeTime = 3f;
//    private float _chaseDistance = 8f;
//    private float _shootDistance = 3f;
//    private float _moveSpeed = 2f;

//    private bool _canShoot = true;


//    private void Update()
//    {
//        float distanceToPlayerX = Mathf.Abs(_player.transform.position.x - transform.position.x);

//        if(distanceToPlayerX < _chaseDistance)
//        {
//            Vector3 directionToPlayer = new Vector3 (_player.transform.position.x - transform.position.x, 0, 0);
//            directionToPlayer.Normalize();
//            directionToPlayer.y = 0;
//            transform.position += directionToPlayer * _moveSpeed * Time.deltaTime;
//        }

//        if (distanceToPlayerX < _shootDistance && _canShoot)
//        {
//            StartCoroutine(ChargeShoot());
//        }
//    }

//    private IEnumerator ChargeShoot()
//    {
//        _canShoot = false;

//        for (int i = 0; i < _featherCnt; i++)
//        {
//            GameObject feather = Instantiate(_featherPrefabs, transform.position, Quaternion.identity);
//            Vector3 directionToPlayer = new Vector3(_player.transform.position.x - transform.position.x, 
//                _player.transform.position.y - transform.position.y, _player.transform.position.z - transform.position.z).normalized;
//            feather.GetComponent<Feather>().SetDirection(directionToPlayer);
//            _moveSpeed = 0;
//            yield return new WaitForSeconds(0.2f);
//        }
//        _moveSpeed = 2f;
//        yield return new WaitForSeconds(_shootCooldown);

//        _canShoot = true;
//    }
//}
