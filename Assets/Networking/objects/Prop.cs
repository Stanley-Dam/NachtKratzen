using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Entity {

    public Rigidbody rigidBody;

    public Player lastTouched;
    public bool isPickedUp = false;
    private bool isPickedUpByOther = false;
    private string objectId;

    private void Start() {
        this.Instantiate(NetworkManager.Instance, true);
        this.objectId = GetGameObjectPath(this.gameObject);
        this.networkManager.AddProp(this);
    }

    private void FixedUpdate() {
        if (isPickedUpByOther)
            return;

        if ((lastTouched != null && networkManager.IsMain(lastTouched.ClientId)) || isPickedUp) {
            if (Vector3.Distance(rigidBody.velocity, Vector3.zero) > 0 || isPickedUp)
                this.networkManager.MoveProp(this, transform.position, transform.rotation);
        }
    }

    public void DontHoldAnymore(Player player) {
        lastTouched = player;
        this.networkManager.MoveProp(this, transform.position, transform.rotation);
    }

    public void MovePropFromServer(Vector3 destinatioLocation, Quaternion rotationDestination, bool isBeingHeld, string clientIdFrom) {
        if (networkManager.Socket.sid == clientIdFrom)
            return;

        if (isBeingHeld) {
            rigidBody.isKinematic = true;
            rigidBody.detectCollisions = false;
            isPickedUpByOther = true;
        } else if (isPickedUpByOther) {
            rigidBody.isKinematic = false;
            rigidBody.detectCollisions = true;
            isPickedUpByOther = false;
            lastTouched = networkManager.GetPlayerFromClientId(clientIdFrom);
        }

        this.transform.position = destinatioLocation;
        this.transform.rotation = rotationDestination;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.root.gameObject.GetComponent<Player>()) {
            this.lastTouched = collision.transform.root.gameObject.GetComponent<Player>();
        }
    }

    public string ObjectId { get { return this.objectId; } }

    private string GetGameObjectPath(GameObject obj) {
        string path = obj.name;
        while (obj.transform.parent != null) {
            obj = obj.transform.parent.gameObject;
            path = obj.name + path;
        }
        return path;
    }

}
