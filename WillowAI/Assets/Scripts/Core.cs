using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectionControllers;
using System.Reflection;
using System;

public class Core : PersistantSingleton<Core>
{
    //public TimeDependantCollection<Entity> Entities = new TimeDependantCollection<Entity>();

    public EntityController EntityController = new EntityController();

    [SerializeField] private GameInitializer gameInitializer;

  //  private Dictionary<Type, CollectionController<Model, View<Model>>> controllers = new Dictionary<Type, CollectionController<Model, View<Model>>>();

    private void Start() {
        EntityController.Setup();
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

    //private CollectionController<Model,View<Model>> GetController(Model instance) {
    //    if (controllers.ContainsKey(instance.GetType())) {
    //        return controllers[instance.GetType()];
    //    } else {
    //        Assembly assembly = typeof(CollectionController<,>).Assembly;
    //        Type controllerType = assembly.GetType(instance.GetType().ToString() + "Controller");

    //        CollectionController<Model,View<Model>> controller = Activator.CreateInstance(controllerType) as CollectionController<Model, View<Model>>;
    //        controller.Setup();

    //        controllers.Add(instance.GetType(), controller);
    //        return controller;
    //    }
    //}
}
