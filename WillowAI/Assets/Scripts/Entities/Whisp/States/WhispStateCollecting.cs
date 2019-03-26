using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispStateCollecting : WhispBaseState {
    private Fragment currentTarget;

    public WhispStateCollecting(Whisp entity, Fragment target) : base(entity) {
        currentTarget = target;
    }

    public override void EnterState() {
        base.EnterState();
        MoveTowardsTarget(currentTarget.Position);
    }

    protected override void OnDestinationReached() {
        base.OnDestinationReached();
        whisp.FragmentSpottingMachine.ResetFocusedTarget();
        currentTarget.DeActivate();
        currentTarget = null;
        GoToExploringState();
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);

        if (currentTarget == null) {
            {
                GoToExploringState();
            }
        }
    }
}