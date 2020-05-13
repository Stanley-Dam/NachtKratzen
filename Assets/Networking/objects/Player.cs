﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Entity {

    private bool isMain;
    private LocalBodyObjects localBodyObjects;

    public void Instantiate(string clientId, bool alive, bool isMain) {
        this.Instantiate(clientId, alive);
        this.isMain = isMain;

        //The player has to get controlled by the server when it isn't the main one.
        if(!this.isMain && this.gameObject.GetComponent<PlayerMovement>()) {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            localBodyObjects = gameObject.GetComponent<LocalBodyObjects>();

            MovePlayer.playerMoveEvent += (player, destination, headRotation) => {
                if(this == player)
                    Move(destination, headRotation);
            };
        }

        TeleportPlayer.playerTeleportEvent += (player, destination, headRotation) => {
            if (this == player)
                Move(destination, headRotation);
        };
    }

    public bool IsMainPlayer { get { return this.isMain; } }

    /// <summary>
    /// This method will be called when the player isn't the main one and get's moved through the server.
    /// </summary>
    /// <param name="destination">The destination the player has to move towards.</param>
    /// <param name="headRotation">The head rotation of the player.</param>
    private void Move(Vector3 destination, Quaternion headRotation) {
        transform.position = destination;
        localBodyObjects.head.rotation = headRotation;
    }

}