using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityViewController : MonoBehaviour
{
    private SubscribtionBasedCollection<Entity> dataCollection {
        get {
            return Core.Instance.Entities;
        }
    }

    private List<EntityView> viewCollection = new List<EntityView>();

    private void Awake() {
        dataCollection.InstanceAddedAction += OnInstanceAdded;
        dataCollection.InstanceRemovedAction += OnInstanceRemoved;
    }

    private void OnInstanceAdded(Entity entity) {
        EntityView view = GetView(entity);
        if (view == null) {
            view = Instantiate(entity.Blueprint.Prefab) as EntityView;
            view.Initialize(entity);
            viewCollection.Add(view);
        }
    }

    private void OnInstanceRemoved(Entity entity) {
        EntityView view = GetView(entity);
        if (view != null) {
            viewCollection.Remove(view);
            Destroy(view.gameObject);
        }
    }

    private EntityView GetView(Entity entity) {
        foreach (EntityView view in viewCollection) {
            if (view.Data == entity) {
                return view;
            }
        }
        return null;
    }
}
