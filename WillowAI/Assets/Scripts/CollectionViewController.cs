using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T> : TimeDependentMonoBehaviour {
    public T Data;

    public void Initialize(T data) {
        this.Data = data;
        OnInitialize();
    }

    protected virtual void OnInitialize() {
    }
}

public abstract class CollectionViewController<T, U> : TimeDependentMonoBehaviour where U : View<T>
{

    protected SubscribtionBasedCollection<T> linkedCollection { get; }
    

}
