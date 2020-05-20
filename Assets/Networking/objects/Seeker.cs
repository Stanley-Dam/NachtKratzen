﻿using System.Collections;
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

    protected override void MoveHandler(int movementType) {
        //Play audio :)
        switch (movementType) {
            case 0:
                break;
            case 1:
                playerAudio.Walk(PlayerAudioType.SEEKER_WALK);
                break;
            case 2:
                playerAudio.Walk(PlayerAudioType.SEEKER_RUN);
                break;
            case 3:
                playerAudio.Jump(PlayerAudioType.SEEKER_JUMP);
                break;
        }
    }

}
