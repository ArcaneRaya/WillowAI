using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : Model<EntityView, Entity>, ISpottable {

    public static Entity CreateEntity(EntityBlueprint blueprint, Vector3 position) {
        switch (blueprint.Type) {
            case EntityBlueprint.EntityType.Whisp:
                return new Whisp(blueprint, position);
            default:
                throw new System.NotImplementedException();
        }
    }

    public SpottingMachine SpottingMachine { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public StateMachine MovementStateMachine { get; private set; }
    public EntityBlueprint EntityBlueprint {
        get {
            return Blueprint as EntityBlueprint;
        }
    }

    public Vector3 Position { get; set; }

    protected abstract BaseState idleState { get; }

    public Entity(EntityBlueprint blueprint, Vector3 position) : base(blueprint) {
        this.Position = position;
        SpottingMachine = new SpottingMachine(this, EntityBlueprint.RangeOfVision);
        StateMachine = new StateMachine();
        MovementStateMachine = new StateMachine();
    }

    protected override void OnActivate() {
        base.OnActivate();
        StateMachine.GoToState(idleState);
    }

    protected override void OnDeActivate() {
        base.OnDeActivate();
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        SpottingMachine.Tick(elapsedTime);
        StateMachine.Tick(elapsedTime);
        MovementStateMachine.Tick(elapsedTime);
    }
}
