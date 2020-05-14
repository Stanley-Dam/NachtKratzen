using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Player {

    private void Awake() {
        RaycastPickup.foundPlayerEvent += KillPlayer;
    }

    private void OnDisable() {
        RaycastPickup.foundPlayerEvent -= KillPlayer;
    }

    private void KillPlayer(GameObject playerGameObject) {
        if(playerGameObject.GetComponent<Hider>())
            this.networkManager.KillPlayer(playerGameObject.GetComponent<Hider>().ClientId);
    }

}
