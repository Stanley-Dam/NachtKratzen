using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

class MovePlayer : PacketHandler, PacketHandlerInterface {

    //Player move event
    public delegate void PlayerMoveEvent(Player player, Vector3 destination, Quaternion headRotation);
    public event PlayerMoveEvent playerMoveEvent;

    private string clientId;

    /// <summary>
    /// Handles a player movement packet by moving the corresponding player object within the game scene.
    /// </summary>
    /// <param name="e">The socket event</param>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public MovePlayer(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.clientId = data["clientId"];

        HandlePacket();
    }

    public void HandlePacket() {
        //TODO call the player move event here :)
    }
}
