using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    protected NetworkManager networkManager;
    private string clientId;
    private bool alive;

    /// <summary>
    /// This function instantiates the entire entity. 
    /// </summary>
    /// <param name="clientId">The clientId of this player's session.</param>
    /// <param name="alive">Wether this entity is alive or not.</param>
    public void Instantiate(string clientId, NetworkManager networkManager, bool alive) {
        this.clientId = clientId;
        this.networkManager = networkManager;
        this.alive = alive;
    }

    public string ClientId { get { return this.clientId; } }
    public bool IsAlive { get { return this.alive; } }
}
