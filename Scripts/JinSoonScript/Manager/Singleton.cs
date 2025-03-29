using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    private static bool isDestroyed = false;

    public static T Instance
    {
        get
        {
            if (isDestroyed)
                instance = null;

            if (instance == null)
            {
                instance = GameObject.FindAnyObjectByType<T>();

                if (instance == null)
                    Debug.LogError($"{typeof(T).Name} Singleton is not exist");
                else
                    isDestroyed = false;
            }
            return instance;
        }
    }

    private void OnDisable()
    {
        isDestroyed = true;
    }
}
