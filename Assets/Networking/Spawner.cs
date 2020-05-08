using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Camera camera;

    [SerializeField] private int mainPlayerLayer = 9;

    private void Awake() {
        PlayerJoin.playerJoinEvent += (clientId, position, headRotation, playerTypeId, isMain) => SpawnPlayer(clientId, position, headRotation, playerTypeId, isMain);
        PlayerQuit.playerQuitEvent += (player) => DeSpawnPlayer(player);
    }

    private void SpawnPlayer(string clientId, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId, bool isMain) {
        GameObject newPlayer = Instantiate(playerPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
        Hider hider = newPlayer.AddComponent<Hider>();
        hider.Instantiate(clientId, true, isMain);

        //Add the camera to this player
        if(isMain) {
            MouseLook mouseLook = camera.gameObject.AddComponent<MouseLook>();
            camera.transform.parent = newPlayer.GetComponent<LocalBodyObjects>().cameraHolder;
            camera.transform.localPosition = new Vector3(0, 0, 0);
            mouseLook.playerBody = newPlayer.transform;
            mouseLook.playerHead = newPlayer.GetComponent<LocalBodyObjects>().head;

            foreach (Transform child in newPlayer.GetComponentsInChildren<Transform>(true)) {
                child.gameObject.layer = mainPlayerLayer;
            }
        }

        networkManager.AddOnlinePlayer(hider);
    }

    private void DeSpawnPlayer(Player player) {
        Destroy(player.gameObject);
    }

}
