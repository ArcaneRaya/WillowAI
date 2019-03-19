using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpottingMachine {

    protected abstract IEnumerable<ISpottable>[] spotCollections { get; }

    public FocusData FocusedObject {
        get {
            return internalFocusedObject;
        }
        private set {
            internalFocusedObject = value;
            if (value != null && NewTargetAction != null) {
                NewTargetAction(internalFocusedObject.VisibleObject.Spottable);
            }
        }
    }

    public Action<ISpottable> NewTargetAction;
    public Action<ISpottable> FocusedOnTargetAction;
    public Action<ISpottable> TargetLostAction;

    protected Entity entity;
    protected float rangeOfVision;
    protected List<VisibleObject> visibleObjects;
    private FocusData internalFocusedObject;

    public SpottingMachine(Entity entity, float rangeOfVision) {
        this.entity = entity;
        this.rangeOfVision = rangeOfVision;
        this.visibleObjects = new List<VisibleObject>();
    }

    public void Tick(float elapsedTime) {
        UpdateVisibleObjects(elapsedTime);
        RedefineTarget();
        UpdateFocusedObject(elapsedTime);
    }

    public bool IsSpottableVisible(ISpottable spottable) {
        VisibleObject visibleObject = visibleObjects.Find((obj) => obj.Spottable == spottable);
        return visibleObject != null;
    }

    protected abstract float GetImportanceMultliplier(ISpottable spottable);

    private void UpdateVisibleObjects(float elapsedTime) {
        List<VisibleObject> oldVisibleObjects = visibleObjects;
        visibleObjects = new List<VisibleObject>();

        foreach (IEnumerable<ISpottable> spottableCollection in spotCollections) {
            ProcessCollection(spottableCollection, oldVisibleObjects, visibleObjects, elapsedTime);
        }
    }

    private void ProcessCollection(IEnumerable<ISpottable> spottableCollection, List<VisibleObject> oldVisibleObjects, List<VisibleObject> visibleObjects, float elapsedTime) {
        foreach (ISpottable spottable in spottableCollection) {
            if (IsSpottableInVision(spottable)) {
                VisibleObject visibleObject = oldVisibleObjects.Find(obj => obj.Spottable == spottable);
                if (visibleObject == null) {
                    visibleObject = new VisibleObject(spottable, 0);
                } else {
                    oldVisibleObjects.Remove(visibleObject);
                }
                visibleObject.TimeVisible += elapsedTime;
                visibleObjects.Add(visibleObject);
            }
        }
    }

    private void RedefineTarget() {
        VisibleObject target = null;
        float targetValue = float.MinValue;
        foreach (VisibleObject visibleObject in visibleObjects) {
            float value = visibleObject.TimeVisible * GetImportanceMultliplier(visibleObject.Spottable);
            if (value > targetValue) {
                targetValue = value;
                target = visibleObject;
            }
        }

        if (target != null) {
            if (FocusedObject == null || FocusedObject.VisibleObject != target) {
                FocusedObject = new FocusData(target);
            }
        }
    }

    private void UpdateFocusedObject(float elapsedTime) {
        if (FocusedObject == null) { return; }
        if (FocusedObject.VisibleObject.Spottable == null) { FocusedObject = null; return; }

        if (IsSpottableInVision(FocusedObject.VisibleObject.Spottable)) {
            FocusedObject.LastSpottedPosition = FocusedObject.VisibleObject.Spottable.Position;
            if (FocusedObject.Progress < 1) {
                FocusedObject.Progress += elapsedTime;
                if (FocusedObject.Progress > 1) {
                    FocusedObject.Progress = 1;
                    if (FocusedOnTargetAction != null) {
                        FocusedOnTargetAction(FocusedObject.VisibleObject.Spottable);
                    }
                }
            }
        } else {
            FocusedObject.Progress -= elapsedTime;
            if (FocusedObject.Progress < 0) {
                TargetLostAction(FocusedObject.VisibleObject.Spottable);
                FocusedObject = null;
            }
        }
    }

    private bool IsSpottableInVision(ISpottable spottable) {
        float sqrDist = (spottable.Position - entity.Position).sqrMagnitude;
        return sqrDist < rangeOfVision * rangeOfVision;
    }

    public class FocusData {
        public VisibleObject VisibleObject;
        public float Progress;
        public Vector3 LastSpottedPosition;

        public FocusData(VisibleObject visibleObject) {
            this.VisibleObject = visibleObject;
            this.Progress = 0;
            this.LastSpottedPosition = visibleObject.Spottable.Position;
        }
    }

    public class VisibleObject {
        public ISpottable Spottable;
        public float TimeVisible;

        public VisibleObject(ISpottable spottable, float timeVisible) {
            this.Spottable = spottable;
            this.TimeVisible = timeVisible;
        }
    }
}

public interface ISpottable {
    Vector3 Position { get; }
}