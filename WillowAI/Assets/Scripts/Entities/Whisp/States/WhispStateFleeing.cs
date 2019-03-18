using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispStateFleeing : WhispBaseState {

    private Player fleeingFromPlayer;

    public WhispStateFleeing(Whisp entity, Player player) : base(entity) {
        fleeingFromPlayer = player;
    }

    public override void EnterState() {
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);
        Vector3 playerDir = fleeingFromPlayer.Position - entity.Position;
        if (playerDir.sqrMagnitude < entity.EntityBlueprint.ImmediateAlertDistance * entity.EntityBlueprint.ImmediateAlertDistance) {
            entity.Position = Vector3.MoveTowards(entity.Position, entity.Position - playerDir, 7.5f * elapsedTime);
        } else {
            GoToAlertState(fleeingFromPlayer);
        }
    }
}
