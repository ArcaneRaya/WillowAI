using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : TimeDependentScript
{

    public static Entity CreateEntity (EntityBlueprint blueprint, Vector3 position) {
        switch (blueprint.Type) {
            case EntityBlueprint.EntityType.Whisp:
                return new Whisp(blueprint, position);
            default:
                throw new System.NotImplementedException();
        }
    }

    public EntityBlueprint Blueprint;
    public Vector3 Position;

    public Entity(EntityBlueprint blueprint, Vector3 position) {
        this.Blueprint = blueprint;
        this.Position = position;
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        Debug.Log("tick");
    }
}
