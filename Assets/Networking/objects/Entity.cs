using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    private string clientId;
    private bool alive;

    /// <summary>
    /// This function instantiates the entire entity. 
    /// </summary>
    /// <param name="clientId">The clientId of this player's session.</param>
    /// <param name="alive">Wether this entity is alive or not.</param>
    /// <param name="location">The location where the entity gets spawned.</param>
    public void Instantiate(string clientId, bool alive, Vector3 location) {
        this.clientId = clientId;
        this.alive = alive;
        this.transform.position = location;
    }

    public string ClientId { get { return this.clientId; } }
    public bool IsAlive { get { return this.alive; } }
}
