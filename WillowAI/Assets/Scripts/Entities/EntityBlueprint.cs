using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityBlueprint : Blueprint<EntityView, Entity> {

    public enum EntityType {
        Whisp,
        Willow
    }

    public EntityType Type;
    public float RangeOfVision;
}
