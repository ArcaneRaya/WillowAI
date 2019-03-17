using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectionControllers;
using System.Reflection;
using System;

public class Core : PersistantSingleton<Core> {
    public EntityController EntityController = new EntityController();

    [SerializeField] private GameInitializer gameInitializer;

    private void Start() {
        EntityController.Activate();
        gameInitializer.InitializeGame();
    }

    public void SubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            EntityController.Subscribe(instance as Entity);
        }
    }

    public void UnSubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            EntityController.UnSubscribe(instance as Entity);
        }
    }

    private void Update() {
        float elapsedTime = Time.deltaTime;

        EntityController.Tick(elapsedTime);
    }

}
