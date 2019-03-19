using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispStateAlert : WhispBaseState {

    private Player spottedPlayer;
    private float timeAlerted;

    public WhispStateAlert(Whisp entity, Player player) : base(entity) {
        spottedPlayer = player;
    }

    public override void EnterState() {
        base.EnterState();
        timeAlerted = 0;
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);

        timeAlerted += elapsedTime;

        Vector3 playerDir = spottedPlayer.Position - entity.Position;
        if (playerDir.sqrMagnitude < entity.EntityBlueprint.FleeDistance * entity.EntityBlueprint.FleeDistance) {
            GoToFleeingState(spottedPlayer);
        } else if (playerDir.sqrMagnitude < entity.EntityBlueprint.ImmediateAlertDistance * entity.EntityBlueprint.ImmediateAlertDistance) {
            entity.Position = Vector3.MoveTowards(entity.Position, entity.Position - playerDir, 4f * elapsedTime);
            timeAlerted = 0;
        } else if (timeAlerted > entity.EntityBlueprint.StayAlertedFor) {
            whisp.TrustedPlayers.Add(new Whisp.TrustedPlayer(spottedPlayer));
            GoToExploringState();
        }
    }
}