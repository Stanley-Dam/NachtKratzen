using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class Player : Entity {

    private string clientId;
    private bool isMain;
    public bool IsMainPlayer { get { return this.isMain; } }

    protected LocalBodyObjects localBodyObjects;
    protected Animator animator;
    protected PlayerAudio playerAudio;

    private InputHandler inputHandler;
    private Transform pickUpParent;
    private Prop pickUp;

    private void OnDestroy() {
        inputHandler.pickup.Disable();
    }

    private void OnDisable() {
        inputHandler.pickup.Disable();
    }

    public void Instantiate(string clientId, NetworkManager networkManager, bool alive, bool isMain) {
        inputHandler = new InputHandler();

        this.Instantiate(networkManager, alive);
        this.clientId = clientId;
        this.localBodyObjects = gameObject.GetComponent<LocalBodyObjects>();
        this.animator = localBodyObjects.anim;
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

            MovePlayerHead.playerMoveHeadEvent += (player, bodyRotation, headRotation) => {
                if (this == player)
                    MoveHead(bodyRotation, headRotation);
            };
        }

        //Display canvas when main player
        this.localBodyObjects.canvas.enabled = isMain;

        if (isMain) {
            inputHandler.pickup.Click.performed += ctx => Pickup();
            inputHandler.pickup.Click.canceled += ctx => StopPickUp();

            inputHandler.pickup.Enable();
        }
    }

    private void StopPickUp() {
        if(pickUp != null) {
            pickUp.rigidBody.isKinematic = false;
            pickUp.rigidBody.detectCollisions = true;
            pickUp.transform.parent = pickUpParent;
            pickUp.isPickedUp = false;
            pickUp.DontHoldAnymore(this);

            pickUp = null;
        }
    }

    private void Pickup() {
        RaycastHit hit;

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 7, layerMask)) {
            if (hit.transform.gameObject.GetComponent<Prop>()) {
                pickUp = hit.transform.gameObject.GetComponent<Prop>();

                pickUp.lastTouched = this;
                pickUp.rigidBody.isKinematic = true;
                pickUp.rigidBody.detectCollisions = false;
                pickUpParent = pickUp.transform.parent;
                pickUp.transform.parent = localBodyObjects.head;
                pickUp.isPickedUp = true;
            }
        }
    }

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

    public void MoveHead(Quaternion bodyRotation, Quaternion headRotation) {
        this.transform.localRotation = bodyRotation;
        this.localBodyObjects.headRotation = headRotation;
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
        this.localBodyObjects.headRotation = headRotation;
        character.enabled = true;
    }

    public string ClientId { get { return this.clientId; } }

}
