using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisp : Entity {

    public SpottingMachine FragmentSpottingMachine { get; protected set; }
    public SpottingMachine PlayerSpottingMachine { get; protected set; }

    public List<TrustedPlayer> TrustedPlayers;

    protected override BaseState idleState {
        get {
            return new WhispStateExploring(this);
        }
    }

    public Whisp(EntityBlueprint blueprint, Vector3 position) : base(blueprint, position) {
        PlayerSpottingMachine = new WhispPlayerSpottingMachine(this, EntityBlueprint.RangeOfVision);
        FragmentSpottingMachine = new WhispFragmentSpottingMachine(this, EntityBlueprint.RangeOfVision);
        TrustedPlayers = new List<TrustedPlayer>();
    }

    protected override void OnTick(float elapsedTime) {
        PlayerSpottingMachine.Tick(elapsedTime);
        FragmentSpottingMachine.Tick(elapsedTime);
        base.OnTick(elapsedTime);

        UpdateTrustedPlayers(elapsedTime);
    }

    private void UpdateTrustedPlayers(float elapsedTime) {
        if (TrustedPlayers.Count == 0) { return; }

        for (int i = TrustedPlayers.Count - 1; i >= 0; i--) {
            TrustedPlayers[i].TimeTrusting += elapsedTime;
            if (TrustedPlayers[i].TimeTrusting > EntityBlueprint.TrustTime) {
                if (PlayerSpottingMachine.IsSpottableVisible(TrustedPlayers[i].Player as ISpottable) == false) {
                    TrustedPlayers.RemoveAt(i);
                }
            }
        }
    }

    protected override void OnActivate() {
        base.OnActivate();
        StateMachine.GoToState(idleState);
        FragmentSpottingMachine.NewTargetAction += OnNewTarget;
        FragmentSpottingMachine.FocusedOnTargetAction += OnTargetFocused;
        FragmentSpottingMachine.TargetLostAction += OnTargetLost;
        PlayerSpottingMachine.NewTargetAction += OnNewTarget;
        PlayerSpottingMachine.FocusedOnTargetAction += OnTargetFocused;
        PlayerSpottingMachine.TargetLostAction += OnTargetLost;
    }

    protected virtual void OnNewTarget(ISpottable target) {
        Debug.Log("new target: " + target.ToString());
    }

    private void OnTargetFocused(ISpottable target) {
        Debug.Log("focused target: " + target.ToString());
    }

    private void OnTargetLost(ISpottable target) {
        Debug.Log("lost target: " + target.ToString());
    }

    protected override void OnDeActivate() {
        base.OnDeActivate();
        FragmentSpottingMachine.NewTargetAction -= OnNewTarget;
        FragmentSpottingMachine.FocusedOnTargetAction -= OnTargetFocused;
        FragmentSpottingMachine.TargetLostAction -= OnTargetLost;
        PlayerSpottingMachine.NewTargetAction -= OnNewTarget;
        PlayerSpottingMachine.FocusedOnTargetAction -= OnTargetFocused;
        PlayerSpottingMachine.TargetLostAction -= OnTargetLost;
    }

    public class TrustedPlayer {
        public Player Player;
        public Vector3 TrustedPosition;
        public float TimeTrusting;

        public TrustedPlayer(Player player) {
            this.Player = player;
            this.TrustedPosition = player.Position;
            this.TimeTrusting = 0;
        }
    }
}


public class WhispFragmentSpottingMachine : SpottingMachine {

    public WhispFragmentSpottingMachine(Entity entity, float rangeOfVision) : base(entity, rangeOfVision) {
    }

    protected override IEnumerable<ISpottable>[] spotCollections {
        get {
            return new IEnumerable<ISpottable>[]{
                Core.Instance.FragmentController.Data.List as IEnumerable<ISpottable>
                };
        }
    }

    protected override float GetImportanceMultliplier(ISpottable spottable) {
        if (spottable is Fragment) {
            float distSqr = (spottable.Position - entity.Position).sqrMagnitude;
            return 1 - (distSqr / (entity.EntityBlueprint.RangeOfVision * entity.EntityBlueprint.RangeOfVision));
        }
        return 1;
    }
}


public class WhispPlayerSpottingMachine : SpottingMachine {

    public WhispPlayerSpottingMachine(Entity entity, float rangeOfVision) : base(entity, rangeOfVision) {
    }

    protected override IEnumerable<ISpottable>[] spotCollections {
        get {
            return new IEnumerable<ISpottable>[]{
                Core.Instance.PlayerController.Data.List as IEnumerable<ISpottable>
                };
        }
    }

    protected override float GetImportanceMultliplier(ISpottable spottable) {
        if (spottable is Player) {
            float distSqr = (spottable.Position - entity.Position).sqrMagnitude;
            return 1 - (distSqr / (entity.EntityBlueprint.RangeOfVision * entity.EntityBlueprint.RangeOfVision));
        }
        return 1;
    }
}