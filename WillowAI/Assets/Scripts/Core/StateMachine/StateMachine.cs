using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    public BaseState currentState;

    public void Tick(float elapsedTime) {
        if (currentState == null) { return; }

        currentState.Tick(elapsedTime);
    }

    public void GoToState(BaseState newState) {
        if (currentState != null) {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }
}
