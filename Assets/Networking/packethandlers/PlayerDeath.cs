using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

class PlayerDeath : PacketHandler, PacketHandlerInterface {

    //Player death event
    public delegate void PlayerDeathEvent(Player player);
    public static event PlayerDeathEvent playerDeathEvent;

    private string clientId;

    /// <summary>
    /// Handles a player dying, this will eventually play an animation and remove the player from the scene.
    /// </summary>
    /// <param name="e">The event we need to handle</param>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public PlayerDeath(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.clientId = data["clientId"];

        HandlePacket();
    }

    public void HandlePacket() {
        if(!networkManager.seeker.IsMainPlayer)
            ((Seeker)networkManager.seeker).PlayPlayerKillAnimation();

        playerDeathEvent(networkManager.GetPlayerFromClientId(this.clientId));
    }
}
