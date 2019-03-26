using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispBaseState : BaseState {

    protected Whisp whisp {
        get {
            return entity as Whisp;
        }
    }
    protected Vector3 cachedPosition;
    protected IMovable trackedMovable;

    public WhispBaseState(Whisp entity) : base(entity) {
    }

    public override void EnterState() {
        base.EnterState();
        entity.Agent.OnDestinationReachedAction += OnDestinationReached;
    }

    public override void ExitState() {
        base.ExitState();
        entity.Agent.OnDestinationReachedAction -= OnDestinationReached;
    }

    public override void Tick(float elapsedTime) {
        base.Tick(elapsedTime);
        if (trackedMovable != null) {
            if ((trackedMovable.Position - cachedPosition).sqrMagnitude > 0.1f) {
                MoveTowardsTarget(trackedMovable.Position);
            }
        }
    }

    protected virtual void OnDestinationReached() {
        trackedMovable = null;
    }

    protected void GoToCollectingState(Fragment target) {
        whisp.StateMachine.GoToState(new WhispStateCollecting(whisp, target));
    }

    protected void GoToAlertState(Player target) {
        whisp.StateMachine.GoToState(new WhispStateAlert(whisp, target));
    }

    protected void GoToFleeingState(Player target) {
        whisp.StateMachine.GoToState(new WhispStateFleeing(whisp, target));
    }

    protected void GoToExploringState() {
        whisp.StateMachine.GoToState(new WhispStateExploring(whisp));
    }

    protected void MoveTowardsTarget(IMovable movable) {
        MoveTowardsTarget(movable.Position);
        trackedMovable = movable;
    }

    protected void MoveTowardsTarget(Vector3 position) {
        entity.Agent.MoveTowards(position);
        cachedPosition = position;
    }
}