using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance {
        get {
            if (instance == null) {
                instance = GetInstance();
            }
            return instance;
        }
    }

    private static T instance;

    private static T GetInstance() {
        T[] instances = FindObjectsOfType<T>();
        if (instances.Length == 1) {
            return instances[0];
        } else if (instances.Length == 0) {
            throw new Exception("No instances found of type " + typeof(T));
        } else {
            throw new Exception("Multiple instances found of type " + typeof(T));
        }
    }

    private void Awake() {
        if (instance == null || instance == this) {
            instance = this as T;
            OnAwake();
        } else {
            Debug.LogWarning(string.Format("Instance of type {0} already exists, destroying object", typeof(T)));
            Destroy(gameObject);
        }
    }

    protected virtual void OnAwake() {

    }
}

public class PersistantSingleton<T> : Singleton<T> where T : PersistantSingleton<T> {
    protected override void OnAwake() {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }
}
