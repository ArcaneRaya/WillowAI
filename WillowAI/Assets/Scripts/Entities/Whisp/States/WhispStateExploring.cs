using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispStateExploring : WhispBaseState {

    public WhispStateExploring(Whisp entity) : base(entity) {
    }

    public override void EnterState() {
        base.EnterState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);
        bool changedState = HandlePlayerSpotting();
        if (!changedState) {
            HandleFragmentSpotting();
        }
    }

    private bool HandlePlayerSpotting() {
        if (whisp.PlayerSpottingMachine.FocusedObject != null) {
            if (whisp.PlayerSpottingMachine.FocusedObject.Progress > 0.5f) {
                ISpottable currentTarget = whisp.PlayerSpottingMachine.FocusedObject.VisibleObject.Spottable;
                Whisp.TrustedPlayer trustedPlayer = whisp.TrustedPlayers.Find(tp => tp.Player == currentTarget as Player);
                if (trustedPlayer != null) {
                    // TODO: only respond when player is moving towards whisp
                    float distSqr = (currentTarget.Position - trustedPlayer.TrustedPosition).sqrMagnitude;
                    if (distSqr > 2 * 2) {
                        whisp.TrustedPlayers.Remove(trustedPlayer);
                        GoToAlertState(currentTarget as Player);
                    }
                } else {
                    float distSqr = (currentTarget.Position - entity.Position).sqrMagnitude;
                    if (distSqr < (entity.EntityBlueprint.FleeDistance * entity.EntityBlueprint.FleeDistance)) {
                        GoToFleeingState(currentTarget as Player);
                        return true;
                    } else if (distSqr < (entity.EntityBlueprint.ImmediateAlertDistance * entity.EntityBlueprint.ImmediateAlertDistance)) {
                        GoToAlertState(currentTarget as Player);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleFragmentSpotting() {
        if (whisp.FragmentSpottingMachine.FocusedObject != null) {
            if (whisp.FragmentSpottingMachine.FocusedObject.Progress >= 1) {
                GoToCollectingState(whisp.FragmentSpottingMachine.FocusedObject.VisibleObject.Spottable as Fragment);
            }
        }
    }
}
