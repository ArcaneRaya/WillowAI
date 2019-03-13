using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribtionBasedCollection<T> {

    protected List<T> internalList;

    public void Subscribe(T instance) {
        if (internalList.Contains(instance) == false) {
            internalList.Add(instance);
            OnInstanceAdded(instance);
        }
    }

    public void UnSubscribe(T instance) {
        if (internalList.Contains(instance)) {
            internalList.Remove(instance);
            OnInstanceRemoved(instance);
        }
    }

    protected virtual void OnInstanceAdded(T instance) {

    }

    protected virtual void OnInstanceRemoved(T instance) {

    }
}

public class TimeDependantCollection<T> : SubscribtionBasedCollection<T> where T : TimeDependentScript {

    public virtual void Tick(float elapsedTime) {
        foreach (T item in internalList) {
            item.Tick(elapsedTime);
        }
    }
}