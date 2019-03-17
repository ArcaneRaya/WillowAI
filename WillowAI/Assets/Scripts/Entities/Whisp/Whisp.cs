using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisp : Entity {

    public Whisp(EntityBlueprint blueprint, Vector3 position) : base(blueprint, position) {
    }

    protected override BaseState idleState {
        get {
            return new WhispStateExploring(this);
        }
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
    }
}
