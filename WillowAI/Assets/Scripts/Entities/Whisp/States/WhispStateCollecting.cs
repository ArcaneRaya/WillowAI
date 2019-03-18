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
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);
        if (currentTarget != null) {
            entity.Position = Vector3.MoveTowards(entity.Position, currentTarget.Position, 4 * elapsedTime);

            float distSqr = (currentTarget.Position - entity.Position).sqrMagnitude;
            if (distSqr < 2 * 2) {
                currentTarget.DeActivate();
                currentTarget = null;
                GoToExploringState();
            }
        }
    }
}
