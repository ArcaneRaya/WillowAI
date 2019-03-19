using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectionControllers;
using System.Reflection;
using System;

public class Core : PersistantSingleton<Core> {
    public PlayerController PlayerController = new PlayerController();
    public EntityController EntityController = new EntityController();
    public FragmentController FragmentController = new FragmentController();

    [SerializeField] private GameInitializer gameInitializer;

    private void Start() {
        PlayerController.Activate();
        EntityController.Activate();
        FragmentController.Activate();
        gameInitializer.InitializeGame();
    }

    public void SubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            EntityController.Subscribe(instance as Entity);
        } else if (instance is Fragment) {
            FragmentController.Subscribe(instance as Fragment);
        } else if (instance is Player) {
            PlayerController.Subscribe(instance as Player);
        } else {
            Debug.LogError("Could not find controller for instance of type " + instance.GetType());
        }
    }

    public void UnSubscribeTimeDependentInstance(ITimeDependent instance) {
        if (instance is Entity) {
            EntityController.UnSubscribe(instance as Entity);
        } else if (instance is Fragment) {
            FragmentController.UnSubscribe(instance as Fragment);
        } else if (instance is Player) {
            PlayerController.UnSubscribe(instance as Player);
        } else {
            Debug.LogError("Could not find controller for instance of type " + instance.GetType());
        }
    }

    private void Update() {
        float elapsedTime = Time.deltaTime;

        EntityController.Tick(elapsedTime);
        FragmentController.Tick(elapsedTime);
        PlayerController.Tick(elapsedTime);
    }

}
