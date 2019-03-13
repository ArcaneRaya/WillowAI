using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : PersistantSingleton<Core>
{
    public TimeDependantCollection<Entity> Entities = new TimeDependantCollection<Entity>();

    public void SubscribeTimeDependentInstance(TimeDependentScript instance) {
        if (instance is Entity) {
            Entities.Subscribe(instance as Entity);
        }
    }

    public void UnSubscribeTimeDependentInstance(TimeDependentScript instance) {
        if (instance is Entity) {
            Entities.UnSubscribe(instance as Entity);
        }
    }
}
