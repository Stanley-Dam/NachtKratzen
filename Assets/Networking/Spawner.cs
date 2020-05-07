using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;

    private void Awake() {
        PlayerJoin.playerJoinEvent += (clientId, position, headRotation, playerTypeId) => SpawnPlayer(clientId, position, headRotation, playerTypeId);
        PlayerQuit.playerQuitEvent += (player) => DeSpawnPlayer(player);
    }

    private void SpawnPlayer(string clientId, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId) {
        GameObject newPlayer = Instantiate(playerPrefab, spawnLocation, headRotation);
        Hider hider = newPlayer.AddComponent<Hider>();
        hider.Instantiate(clientId, true);

        networkManager.AddOnlinePlayer(hider);
    }

    private void DeSpawnPlayer(Player player) {
        Destroy(player.gameObject);
    }

}
