using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Entity {

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private Rigidbody rigidBody;

    private int objectId = 0;

    private void Awake() {
        this.Instantiate(networkManager, true);
        this.objectId = this.gameObject.GetInstanceID();
        this.networkManager.AddProp(this);
    }

    private void FixedUpdate() {
        if(Vector3.Distance(rigidBody.velocity, Vector3.zero) > 0)
            this.networkManager.MoveProp(this, transform.position, transform.rotation);
    }

    public int ObjectId { get { return this.objectId; } }

}
