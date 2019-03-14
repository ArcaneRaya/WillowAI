using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityBlueprint : ScriptableObject {

    public enum EntityType {
        Whisp,
        Willow
    }

    public EntityType Type;
    public EntityView Prefab;
}
