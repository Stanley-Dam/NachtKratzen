using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Player {

    private void Awake() {
        RaycastPickup.foundPlayerEvent += playerGameObject => this.networkManager.KillPlayer(playerGameObject.GetComponent<Hider>().ClientId);
    }

}
