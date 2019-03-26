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
        MoveAwayFromPlayer();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);
        Vector3 playerDir = fleeingFromPlayer.Position - entity.Position;
        if (playerDir.sqrMagnitude < entity.EntityBlueprint.ImmediateAlertDistance * entity.EntityBlueprint.ImmediateAlertDistance) {
            MoveAwayFromPlayer();
        } else {
            whisp.Agent.Stop();
            GoToAlertState(fleeingFromPlayer);
        }
    }

    private void MoveAwayFromPlayer() {
        Vector3 playerDir = (fleeingFromPlayer.Position - entity.Position).normalized;
        MoveTowardsTarget(whisp.Position - playerDir * 3);
    }

    protected override void OnDestinationReached() {
        base.OnDestinationReached();
        MoveAwayFromPlayer();
    }
}
