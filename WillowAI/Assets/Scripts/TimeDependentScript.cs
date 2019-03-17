using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeDependent {
    void Activate();
    void Tick(float elapsedTime);
    void DeActivate();
}

public class Model<U, T> : ITimeDependent where U : View<T> where T : Model<U, T> {
    public Blueprint<U, T> Blueprint;

    public Model(Blueprint<U, T> blueprint) {
        Blueprint = blueprint;
    }

    public void Activate() {
        Core.Instance.SubscribeTimeDependentInstance(this);
        OnActivate();
    }

    public void Tick(float elapsedTime) {
        OnTick(elapsedTime);
    }

    public void DeActivate() {
        Core.Instance.UnSubscribeTimeDependentInstance(this);
        OnDeActivate();
    }

    protected virtual void OnActivate() { }

    protected virtual void OnTick(float elapsedTime) { }

    protected virtual void OnDeActivate() { }
}

public class TimeDependentMonoBehaviour : MonoBehaviour, ITimeDependent {
    public void Activate() {
        Core.Instance.SubscribeTimeDependentInstance(this);
        OnSetup();
    }

    public void DeActivate() {
        Core.Instance.UnSubscribeTimeDependentInstance(this);
        OnStop();
    }

    public void Tick(float elapsedTime) {
        OnTick(elapsedTime);
    }

    protected virtual void OnSetup() { }

    protected virtual void OnTick(float elapsedTime) { }

    protected virtual void OnStop() { }
}