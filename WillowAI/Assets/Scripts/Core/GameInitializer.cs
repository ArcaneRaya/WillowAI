using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    [SerializeField] private PlayerBlueprint playerBlueprint;
    [SerializeField] private EntityBlueprint whispBlueprint;
    [SerializeField] private FragmentBlueprint fragmentBlueprint;

    public void InitializeGame() {
        CreatePlayers(1);
        CreateWhisps(1);
        CreateFragments(5);
    }

    private void CreatePlayers(int amount) {
        for (int i = 0; i < amount; i++) {
            Player player = Player.Create(playerBlueprint, new Vector3(0, 0, 0));
            player.Activate();
        }
    }

    private void CreateWhisps(int amount) {
        for (int i = 0; i < amount; i++) {
            Entity whisp = Entity.CreateEntity(whispBlueprint, new Vector3(i * 3, 0, 3));
            whisp.Activate();
        }
    }

    private void CreateFragments(int amount) {
        for (int i = 0; i < amount; i++) {
            Fragment fragment = Fragment.Create(fragmentBlueprint, new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10)));
            fragment.Activate();
        }
    }
}
