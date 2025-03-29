using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDamageManager : Singleton<CleanDamageManager>
{
    [SerializeField] GameObject _damageCheckObj;
    [SerializeField] Transform _playerTransform;
    [SerializeField] GameObject _particle;

    public void DamageObject()
    {
        GameObject instantiatedObj = Instantiate(_damageCheckObj, _playerTransform.position, _playerTransform.rotation);
        GameObject instantiatedParticle = Instantiate(_particle, _playerTransform);
        Debug.Log(instantiatedObj);
        StartCoroutine(DeleteRoutine(instantiatedObj, instantiatedParticle));
    }

    IEnumerator DeleteRoutine(GameObject instantiatedObj, GameObject instantiatedParticle)
    {
        yield return new WaitForSeconds(1f);
        Destroy(instantiatedObj);
        Destroy(instantiatedObj);
        Debug.Log(instantiatedObj); 
    }
}