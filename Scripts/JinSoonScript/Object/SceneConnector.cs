using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConnector : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerPrefs.SetInt("IsTutorialed", 1);
            SceneManager.LoadScene(_sceneName);
        }
    }
}
