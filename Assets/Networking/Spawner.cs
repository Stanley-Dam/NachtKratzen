using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject seekerPrefab;
    [SerializeField] private new Camera camera;

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
                newPlayer = Instantiate(playerPrefab, spawnLocation, new Quaternion());
                Hider hider = newPlayer.AddComponent<Hider>();
                hider.Instantiate(clientId, networkManager, true, isMain);
                player = hider;
                break;
            case 1:
                newPlayer = Instantiate(seekerPrefab, spawnLocation, new Quaternion());
                Seeker seeker = newPlayer.AddComponent<Seeker>();
                seeker.Instantiate(clientId, networkManager, true, isMain);
                player = seeker;
                networkManager.seeker = seeker;
                break;
        }

        //Add the camera to this player
        if (player.IsMainPlayer) {
            camera.transform.parent = newPlayer.GetComponent<LocalBodyObjects>().cameraHolder;
            camera.transform.localPosition = new Vector3(0, 0, 0);

            SmoothMouseLook mouseLook = camera.gameObject.AddComponent<SmoothMouseLook>();
            mouseLook.playerBody = newPlayer.transform;
            mouseLook.localBodyObjects = newPlayer.GetComponent<LocalBodyObjects>();

            foreach (Transform child in newPlayer.GetComponentsInChildren<Transform>(true)) {
                child.gameObject.layer = mainPlayerLayer;
            }
        }

        networkManager.AddOnlinePlayer(player);
    }

    private void SetSeeker(Player player) {
        string clientId = player.ClientId;
        Vector3 location = player.transform.position;
        Quaternion headRotation = player.gameObject.GetComponent<LocalBodyObjects>().headRotation;
        bool isMain = player.IsMainPlayer;

        DeSpawnPlayer(player);
        SpawnPlayer(clientId, location, headRotation, 1, isMain);
        networkManager.RemoveOnlinePlayer(player);
    }

    private void DeSpawnPlayer(Player player) {
        if (player.IsMainPlayer) {
            Destroy(camera.gameObject.GetComponent<SmoothMouseLook>());
            camera.transform.parent = null;
        }

        Destroy(player.gameObject);
    }

    private void PlayerDeathHandler(Player player) {
        if (player is Hider)
            ((Hider)player).PlayDeathAnimation();

        bool setCam = false;
        if (player.IsMainPlayer) {
            camera.transform.parent = null;
            setCam = true;
            Destroy(player.gameObject.GetComponent<PlayerMovement>());
            foreach (Transform child in player.gameObject.GetComponentsInChildren<Transform>(true)) {
                child.gameObject.layer = 0;
            }
        }

        networkManager.RemoveOnlinePlayer(player);
        Destroy(player.gameObject.GetComponent<CharacterController>());
        Destroy(player.gameObject.GetComponent<CapsuleCollider>());
        Destroy(player.gameObject.GetComponent<LocalBodyObjects>());
        Destroy(player);

        if (setCam) {
            camera.gameObject.GetComponent<SmoothMouseLook>().enabled = false;

            Player selectedPlayer = networkManager.seeker;

            camera.transform.parent = selectedPlayer.gameObject.GetComponent<LocalBodyObjects>().head;
            camera.transform.localPosition = new Vector3(0, 0, 0);
            camera.transform.localRotation = Quaternion.Euler(-60, -90, 0);

            foreach (Transform child in selectedPlayer.gameObject.GetComponentsInChildren<Transform>(true)) {
                child.gameObject.layer = mainPlayerLayer;
            }
        }
    }

}
