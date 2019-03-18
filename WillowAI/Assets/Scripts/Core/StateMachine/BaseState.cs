using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState {
    protected Entity entity;

    public BaseState(Entity entity) {
        this.entity = entity;
    }

    public virtual void EnterState() {

    }

    public virtual void Tick(float elapsedTime) {

    }

    public virtual void ExitState() {

    }
}
