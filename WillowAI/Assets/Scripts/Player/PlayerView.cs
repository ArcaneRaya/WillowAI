using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : View<Player> {

    protected override void OnInitialize() {
        base.OnInitialize();
        transform.position = Data.Position;
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        transform.position = Data.Position;
    }
}
