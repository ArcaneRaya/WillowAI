using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Model<PlayerView, Player>, ISpottable {

    public Vector3 Position { get; private set; }

    protected PlayerBlueprint playerBlueprint {
        get {
            return Blueprint as PlayerBlueprint;
        }
    }

    public static Player Create(PlayerBlueprint playerBlueprint, Vector3 position) {
        return new Player(playerBlueprint, position);
    }

    public Player(Blueprint<PlayerView, Player> blueprint, Vector3 position) : base(blueprint) {
        this.Position = position;
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);

        //TODO remove hacky shit
        if (Input.GetKey(KeyCode.W)) {
            Position += Vector3.forward * playerBlueprint.MovementSpeed * elapsedTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            Position += Vector3.left * playerBlueprint.MovementSpeed * elapsedTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            Position += Vector3.back * playerBlueprint.MovementSpeed * elapsedTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            Position += Vector3.right * playerBlueprint.MovementSpeed * elapsedTime;
        }
    }
}
