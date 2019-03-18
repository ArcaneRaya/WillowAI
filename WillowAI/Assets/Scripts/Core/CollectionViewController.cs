using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T> : TimeDependentMonoBehaviour {
    public T Data;

    public void Initialize(T data) {
        this.Data = data;
        OnInitialize();
    }

    protected virtual void OnInitialize() {
    }
}

namespace CollectionControllers {

    public class CollectionController<T, U> : ITimeDependent where U : View<T> where T : Model<U, T> {

        public TimeDependantCollection<T> Data = new TimeDependantCollection<T>();
        public TimeDependantCollection<U> Views = new TimeDependantCollection<U>();

        protected Transform transform;

        public void Activate() {
            transform = new GameObject(typeof(U).ToString() + " - instances").transform;
            Data.InstanceAddedAction += OnDataInstanceAdded;
            Data.InstanceRemovedAction += OnDataInstanceRemoved;
        }

        public void Tick(float elapsedTime) {
            Data.Tick(elapsedTime);
            Views.Tick(elapsedTime);
        }

        public void DeActivate() {

        }

        public void Subscribe(T data) {
            Data.Subscribe(data);
        }

        public void UnSubscribe(T data) {
            Data.UnSubscribe(data);
        }

        private void OnDataInstanceAdded(T obj) {
            U view = GetView(obj);
            if (view == null) {
                view = GameObject.Instantiate(obj.Blueprint.Prefab, transform) as U;
                view.Initialize(obj);
                Views.Subscribe(view);
            }
        }

        private void OnDataInstanceRemoved(T obj) {
            U view = GetView(obj);
            if (view != null) {
                Views.UnSubscribe(view);
                GameObject.Destroy(view.gameObject);
            }
        }

        private U GetView(T obj) {
            foreach (U view in Views.List) {
                if (view.Data.Equals(obj)) {
                    return view;
                }
            }
            return null;
        }
    }

}