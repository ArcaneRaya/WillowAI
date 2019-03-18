using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Model<PlayerView, Player>, ISpottable {

    public Vector3 Position { get; private set; }

    public static Player Create(PlayerBlueprint playerBlueprint, Vector3 position) {
        return new Player(playerBlueprint, position);
    }

    public Player(Blueprint<PlayerView, Player> blueprint, Vector3 position) : base(blueprint) {
        this.Position = position;
    }
}
