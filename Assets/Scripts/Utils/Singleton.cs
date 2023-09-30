using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static volatile T _instance;
    public static T Instance
    {
        get
        {
              
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
            }
            
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
}

