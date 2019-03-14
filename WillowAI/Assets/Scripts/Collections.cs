using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribtionBasedCollection<T> {

    public Action<T> InstanceAddedAction;
    public Action<T> InstanceRemovedAction;

    protected List<T> internalList = new List<T>();

    public void Subscribe(T instance) {
        if (internalList.Contains(instance) == false) {
            internalList.Add(instance);
            OnInstanceAdded(instance);
            if (InstanceAddedAction != null) {
                InstanceAddedAction(instance);
            }
        }
    }

    public void UnSubscribe(T instance) {
        if (internalList.Contains(instance)) {
            internalList.Remove(instance);
            OnInstanceRemoved(instance);
            if (InstanceRemovedAction != null) {
                InstanceRemovedAction(instance);
            }
        }
    }

    protected virtual void OnInstanceAdded(T instance) {
        
    }

    protected virtual void OnInstanceRemoved(T instance) {

    }
}

public class TimeDependantCollection<T> : SubscribtionBasedCollection<T> where T : ITimeDependent {

    public virtual void Tick(float elapsedTime) {
        foreach (T item in internalList) {
            item.Tick(elapsedTime);
        }
    }
}