using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Entity {

    private string clientId;
    private bool isMain;
    protected LocalBodyObjects localBodyObjects;
    protected Animator animator;
    protected PlayerAudio playerAudio;

    public void Instantiate(string clientId, NetworkManager networkManager, bool alive, bool isMain) {
        this.Instantiate(networkManager, alive);
        this.clientId = clientId;
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
    private void Move(Vector3 destination, int movementType) {
        this.transform.position = destination;

        MoveHandler(movementType);
    }

    protected virtual void MoveHandler(int movementType) {
        //Can be handled by classes inheriting this class :)
    }

    public void MoveHead(Quaternion headRotation) {
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

    public string ClientId { get { return this.clientId; } }

}
