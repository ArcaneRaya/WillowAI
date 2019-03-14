using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    [SerializeField] private EntityBlueprint whispBlueprint;

    public void InitializeGame() {
        CreateWhisps(10);
    }

    private void CreateWhisps(int amount) {
        for (int i = 0; i < amount; i++) {
            Entity whisp = Entity.CreateEntity(whispBlueprint, new Vector3(i, 0, 0));
            whisp.Setup();
        }
    }
}
