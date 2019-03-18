using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispBaseState : BaseState {

    protected Whisp whisp {
        get {
            return entity as Whisp;
        }
    }

    public WhispBaseState(Whisp entity) : base(entity) {
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
}
