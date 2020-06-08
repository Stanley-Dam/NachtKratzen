using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    protected NetworkManager networkManager;
    private bool alive;

    /// <summary>
    /// This function instantiates the entire entity. 
    /// </summary>
    /// <param name="clientId">The clientId of this player's session.</param>
    /// <param name="alive">Wether this entity is alive or not.</param>
    public void Instantiate(NetworkManager networkManager, bool alive) {
        this.networkManager = networkManager;
        this.alive = alive;
    }

    public bool IsAlive { get { return this.alive; } }
}
