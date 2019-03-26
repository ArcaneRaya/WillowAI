using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent {

    public Action OnDestinationReachedAction;

    private PathfindingController pathfindingController;
    private Entity entity;

    private Queue<PathfindingNode> currentPath;

    public PathfindingAgent(Entity entity) {
        pathfindingController = GameObject.FindObjectOfType<PathfindingController>();
        this.entity = entity;
        this.currentPath = new Queue<PathfindingNode>();
        this.entity.Position = pathfindingController.Grid.GetClosestNode(entity.Position).WorldPosition;
    }

    public void Tick(float elapsedTime) {
        if (currentPath.Count > 0) {
            UpdateMovement(elapsedTime);
        }
    }

    public void MoveTowards(Vector3 targetPosition) {
        Stop();

        PathfindingNode targetNode = pathfindingController.Grid.GetClosestNode(targetPosition);
        PathfindingNode currentNode = pathfindingController.Grid.GetClosestNode(entity.Position);

        currentPath = pathfindingController.CalculatePath(currentNode, targetNode);
    }

    private void UpdateMovement(float elapsedTime) {
        float traversableDistance = entity.EntityBlueprint.MoveSpeed * elapsedTime;
        PathfindingNode currentTarget = currentPath.Peek();
        float sqrDistTarget = (currentTarget.WorldPosition - entity.Position).sqrMagnitude;
        // remove node from path if entity is close enough and can move further;
        if (sqrDistTarget < traversableDistance * traversableDistance) {
            if (currentPath.Count > 1) {
                currentPath.Dequeue();
                currentTarget = currentPath.Peek();
            } else {
                currentPath.Dequeue();
                entity.Position = currentTarget.WorldPosition;
                if (OnDestinationReachedAction != null) {
                    OnDestinationReachedAction();
                }
                return;
            }
        }

        Vector3 movementDirection = currentTarget.WorldPosition - entity.Position;
        entity.Position += movementDirection.normalized * entity.EntityBlueprint.MoveSpeed * elapsedTime;
    }

    public void Stop() {
        currentPath.Clear();
    }
}
