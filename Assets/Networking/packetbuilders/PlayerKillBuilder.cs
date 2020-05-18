using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillBuilder : PacketBuilder {

    /// <summary>
    /// When the main player is the seeker and has found someone
    /// </summary>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The networkmanager</param>
    /// <param name="clientId">The clientId of the killed player</param>
    public PlayerKillBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager, string clientId) :
    base(socket, networkManager, "PlayerDeath") {
        data["clientId"] = clientId;

        Send();
    }

}
