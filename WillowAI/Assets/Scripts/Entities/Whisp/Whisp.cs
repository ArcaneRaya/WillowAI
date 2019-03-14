using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisp : Entity {

    public Whisp(EntityBlueprint blueprint, Vector3 position) : base(blueprint, position) {
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        Position.x += 10 * elapsedTime;
    }
}
