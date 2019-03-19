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
    public float RangeOfVision = 50;
    public float FleeDistance = 10;
    public float ImmediateAlertDistance = 20;
    public float StayAlertedFor = 3;
    public float TrustTime = 8;
}
