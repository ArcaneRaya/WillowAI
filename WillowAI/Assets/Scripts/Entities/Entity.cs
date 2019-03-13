using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : TimeDependentScript
{
    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        Debug.Log("tick");
    }
}
