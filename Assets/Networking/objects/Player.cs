using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Entity {

    private bool isMain;

    public void Instantiate(string clientId, bool alive, bool isMain) {
        this.Instantiate(clientId, alive);
        this.isMain = isMain;

        if(!this.isMain && this.gameObject.GetComponent<PlayerMovement>()) {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;

            MovePlayer.playerMoveEvent += (player, destination, headRotation) => {
                if(this == player)
                    Move(destination, headRotation);
            };
        }
    }

    public bool IsMainPlayer { get { return this.isMain; } }

    private void Move(Vector3 destination, Quaternion headRotation) {
        transform.position = destination;
        gameObject.GetComponent<LocalBodyObjects>().head.rotation = headRotation;
    }

}
