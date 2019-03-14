using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : PersistantSingleton<Core>
{
    public TimeDependantCollection<Entity> Entities = new TimeDependantCollection<Entity>();

    [SerializeField] private GameInitializer gameInitializer;

    private void Start() {
        gameInitializer.InitializeGame();
    }

    public void SubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            Entities.Subscribe(instance as Entity);
        }
    }

    public void UnSubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            Entities.UnSubscribe(instance as Entity);
        }
    }
}
