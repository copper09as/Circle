using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingleTon<T>: MonoBehaviour where T :MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).ToString());
                    _instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }


    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
