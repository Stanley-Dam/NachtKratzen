using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject seekerPrefab;
    [SerializeField] private Camera camera;

    [SerializeField] private int mainPlayerLayer = 9;

    private void Awake() {
        PlayerJoin.playerJoinEvent += (clientId, position, headRotation, playerTypeId, isMain) => SpawnPlayer(clientId, position, headRotation, playerTypeId, isMain);
        PlayerQuit.playerQuitEvent += (player) => DeSpawnPlayer(player);
        UpdateSeeker.updateSeekerEvent += (player) => SetSeeker(player);
    }

    private void SpawnPlayer(string clientId, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId, bool isMain) {

        GameObject newPlayer = null;
        Player player = null;

        switch (playerTypeId) {
            case 0:
                newPlayer = Instantiate(playerPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
                Hider hider = newPlayer.AddComponent<Hider>();
                hider.Instantiate(clientId, true, isMain);
                player = hider;
                break;
            case 1:
                newPlayer = Instantiate(seekerPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
                Seeker seeker = newPlayer.AddComponent<Seeker>();
                seeker.Instantiate(clientId, true, isMain);
                player = seeker;
                break;
        }

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

        networkManager.AddOnlinePlayer(player);
    }

    private void SetSeeker(Player player) {
        string clientId = player.ClientId;
        Vector3 location = player.transform.position;
        Quaternion headRotation = player.gameObject.GetComponent<LocalBodyObjects>().head.rotation;
        bool isMain = player.IsMainPlayer;

        DeSpawnPlayer(player);
        networkManager.RemoveOnlinePlayer(player);
        SpawnPlayer(clientId, location, headRotation, 1, isMain);
    }

    private void DeSpawnPlayer(Player player) {
        Destroy(player.gameObject);
    }

}
