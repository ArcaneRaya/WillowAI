﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathfindingController))]
public class PathfindingContoller_Editor : Editor {

    private SerializedProperty grid;
    private SerializedObject serializedGridObject;

    private SerializedProperty gridProperty_floor;
    private SerializedProperty gridProperty_obstacle;
    private SerializedProperty gridProperty_NodeSize;
    private SerializedProperty gridProperty_UnitHeight;


    void OnEnable() {
        grid = serializedObject.FindProperty("pathfindingGrid");
        serializedGridObject = new SerializedObject(grid.objectReferenceValue);

        gridProperty_floor = serializedGridObject.FindProperty("Floor");
        gridProperty_obstacle = serializedGridObject.FindProperty("Obstacle");
        gridProperty_NodeSize = serializedGridObject.FindProperty("NodeSize");
        gridProperty_UnitHeight = serializedGridObject.FindProperty("UnitHeight");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        serializedGridObject.Update();
        EditorGUILayout.PropertyField(gridProperty_floor);
        EditorGUILayout.PropertyField(gridProperty_obstacle);
        EditorGUILayout.PropertyField(gridProperty_NodeSize);
        EditorGUILayout.PropertyField(gridProperty_UnitHeight);
        serializedGridObject.ApplyModifiedProperties();

    }
}