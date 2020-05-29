using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject seekerPrefab;
    [SerializeField] private GameObject cam;

    [SerializeField] private int mainPlayerLayer = 9;

    private void Awake() {
        PlayerJoin.playerJoinEvent += SpawnPlayer;
        PlayerQuit.playerQuitEvent += DeSpawnPlayer;
        UpdateSeeker.updateSeekerEvent += SetSeeker;
        PlayerDeath.playerDeathEvent += PlayerDeathHandler;
    }

    private void OnDisable() {
        PlayerJoin.playerJoinEvent -= SpawnPlayer;
        PlayerQuit.playerQuitEvent -= DeSpawnPlayer;
        UpdateSeeker.updateSeekerEvent -= SetSeeker;
        PlayerDeath.playerDeathEvent -= PlayerDeathHandler;
    }

    private void SpawnPlayer(string clientId, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId, bool isMain) {
        GameObject newPlayer = null;
        Player player = null;

        switch (playerTypeId) {
            case 0:
                newPlayer = Instantiate(playerPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
                Hider hider = newPlayer.AddComponent<Hider>();
                hider.Instantiate(clientId, networkManager, true, isMain);
                player = hider;
                break;
            case 1:
                newPlayer = Instantiate(seekerPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
                Seeker seeker = newPlayer.AddComponent<Seeker>();
                seeker.Instantiate(clientId, networkManager, true, isMain);
                player = seeker;
                networkManager.seeker = seeker;
                break;
        }

        player.MoveHead(headRotation);

        //Add the camera to this player
        if(isMain) {
            cam = Camera.main.gameObject;
            cam.transform.parent = newPlayer.GetComponent<LocalBodyObjects>().cameraHolder;
            cam.transform.localPosition = new Vector3(0, 0, 0);

            MouseLook mouseLook = cam.AddComponent<MouseLook>();
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
        if (player.IsMainPlayer) {
            cam.gameObject.GetComponent<MouseLook>().enabled = false;
        }

        Destroy(player.gameObject);
    }

    private void PlayerDeathHandler(Player player) {
        bool setCam = false;
        if (player.IsMainPlayer) {
            cam.transform.parent = null;
            setCam = true;
        }

        networkManager.RemoveOnlinePlayer(player);
        Destroy(player.gameObject);

        if(setCam) {
            cam.gameObject.GetComponent<MouseLook>().enabled = false;

            Player selectedPlayer = networkManager.seeker;

            cam.transform.parent = selectedPlayer.gameObject.GetComponent<LocalBodyObjects>().head;
            cam.transform.localPosition = new Vector3(0, 0, 0);
            cam.transform.localRotation = Quaternion.Euler(0, 0, 0);

            foreach (Transform child in selectedPlayer.gameObject.GetComponentsInChildren<Transform>(true)) {
                child.gameObject.layer = mainPlayerLayer;
            }
        }
    }

}
