﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Entity {

    private bool isMain;
    private LocalBodyObjects localBodyObjects;
    private Animator animator;
    private PlayerAudio playerAudio;

    public void Instantiate(string clientId, NetworkManager networkManager, bool alive, bool isMain) {
        this.Instantiate(clientId, networkManager, alive);
        this.localBodyObjects = gameObject.GetComponent<LocalBodyObjects>();
        this.animator = gameObject.GetComponent<Animator>();
        this.playerAudio = gameObject.GetComponent<PlayerAudio>();
        this.isMain = isMain;

        TeleportPlayer.playerTeleportEvent += (player, destination, headRotation) => {
            if (this == player)
                Teleport(destination, headRotation);
        };

        //The player has to get controlled by the server when it isn't the main one.
        if (!this.isMain && this.gameObject.GetComponent<PlayerMovement>()) {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;

            MovePlayer.playerMoveEvent += (player, destination, movementType) => {
                if(this == player)
                    Move(destination, movementType);
            };

            MovePlayerHead.playerMoveHeadEvent += (player, headRotation) => {
                if (this == player)
                    MoveHead(headRotation);
            };
        }
    }

    public bool IsMainPlayer { get { return this.isMain; } }

    /// <summary>
    /// This method will be called when the player isn't the main one and get's moved through the server.
    /// </summary>
    /// <param name="destination">The destination the player has to move towards.</param>
    /// <param name="headRotation">The head rotation of the player.</param>
    private void Move(Vector3 destination, int movementType) {
        this.transform.position = destination;

        //Play audio :)
        //And animation
        switch (movementType) {
            case 0:
                animator.SetFloat("Speed", 0f);
                break;
            case 1:
                playerAudio.Walk(PlayerAudioType.WALK_AUDIO_CONCRETE);
                animator.SetFloat("Speed", 0.5f);
                break;
            case 2:
                playerAudio.Walk(PlayerAudioType.RUN_AUDIO_CONCRETE);
                animator.SetFloat("Speed", 1f);
                break;
        }
    }

    private void MoveHead(Quaternion headRotation) {
        Vector3 euler = headRotation.eulerAngles;
        this.transform.rotation = Quaternion.Euler(0, euler.y, 0);
        this.localBodyObjects.head.localRotation = Quaternion.Euler(euler.x, 0, 0);
    }

    /// <summary>
    /// This method can only be called by the server, it will teleport the given player to the given destination.
    /// </summary>
    /// <param name="destination">The location we're teleporting the player towards.</param>
    /// <param name="headRotation">The headrotation of the player</param>
    private void Teleport(Vector3 destination, Quaternion headRotation) {
        CharacterController character = this.GetComponent<CharacterController>();
        character.enabled = false;
        this.transform.position = destination;
        this.localBodyObjects.head.rotation = headRotation;
        character.enabled = true;
    }

}