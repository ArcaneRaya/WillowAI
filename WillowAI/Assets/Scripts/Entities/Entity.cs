using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : Model<EntityView, Entity>, ISpottable, IMovable {

    public static Entity CreateEntity(EntityBlueprint blueprint, Vector3 position) {
        switch (blueprint.Type) {
            case EntityBlueprint.EntityType.Whisp:
                return new Whisp(blueprint, position);
            default:
                throw new System.NotImplementedException();
        }
    }

    public StateMachine StateMachine { get; protected set; }
    public StateMachine MovementStateMachine { get; protected set; }
    public PathfindingAgent Agent { get; protected set; }

    public EntityBlueprint EntityBlueprint {
        get {
            return Blueprint as EntityBlueprint;
        }
    }

    public Vector3 Position { get; set; }

    protected abstract BaseState idleState { get; }

    public Entity(EntityBlueprint blueprint, Vector3 position) : base(blueprint) {
        this.Position = position;
        StateMachine = new StateMachine();
        MovementStateMachine = new StateMachine();
        Agent = new PathfindingAgent(this);
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        StateMachine.Tick(elapsedTime);
        MovementStateMachine.Tick(elapsedTime);
        Agent.Tick(elapsedTime);
    }
}
