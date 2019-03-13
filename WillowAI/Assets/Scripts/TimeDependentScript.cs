using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDependentScript
{
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
