using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willow : Entity {

    public SpottingMachine WhispSpottingMachine { get; protected set; }
    public SpottingMachine PlayerSpottingMachine { get; protected set; }

    protected override BaseState idleState {
        get {
            throw new System.NotImplementedException();
        }
    }

    public Willow(EntityBlueprint blueprint, Vector3 position) : base(blueprint, position) {
    }
}
