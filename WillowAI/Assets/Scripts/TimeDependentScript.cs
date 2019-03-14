using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeDependent {
    void Setup();
    void Tick(float elapsedTime);
    void Stop();
}

public class Model<U,T> : ITimeDependent where U : View<T> where T : Model<U,T> {
    public Blueprint<U,T> Blueprint;

    public void Setup() {
        Core.Instance.SubscribeTimeDependentInstance(this);
        OnSetup();
    }

    public void Tick(float elapsedTime) {
        OnTick(elapsedTime);
    }

    public void Stop() {
        Core.Instance.UnSubscribeTimeDependentInstance(this);
        OnStop();
    }

    protected virtual void OnSetup() { }

    protected virtual void OnTick(float elapsedTime) { }

    protected virtual void OnStop() { }
}

public class TimeDependentMonoBehaviour : MonoBehaviour, ITimeDependent {
    public void Setup() {
        Core.Instance.SubscribeTimeDependentInstance(this);
        OnSetup();
    }

    public void Stop() {
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