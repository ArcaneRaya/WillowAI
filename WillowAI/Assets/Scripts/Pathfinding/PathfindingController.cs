using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour {

    [SerializeField] private PathfindingGrid pathfindingGrid;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDrawGizmosSelected() {
        if (pathfindingGrid == null) { return; }
        if (pathfindingGrid.rowAmount == 0 || pathfindingGrid.columnAmount == 0) { return; }
        if (pathfindingGrid.Nodes == null || pathfindingGrid.Nodes.Length == 0) { return; }

        for (int i = 0; i < pathfindingGrid.Nodes.Length; i++) {
            PathfindingNode node = pathfindingGrid.Nodes[i];

            Vector3 center = node.WorldPosition;
            Vector3 size = Vector3.one * pathfindingGrid.NodeSize;

            if (node.IsObstacle) {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(center, size);
            } else if (node.IsWalkable) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(center, size);
            } else {
                continue;
            }
        }
    }
}
