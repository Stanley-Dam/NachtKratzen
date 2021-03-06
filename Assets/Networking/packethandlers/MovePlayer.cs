﻿using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

class MovePlayer : PacketHandler, PacketHandlerInterface {

    //Player move event
    public delegate void PlayerMoveEvent(Player player, Vector3 destination, int movementType);
    public static event PlayerMoveEvent playerMoveEvent;

    private string clientId;
    private int movementType;
    private float x;
    private float y;
    private float z;

    /// <summary>
    /// Handles a player movement packet by moving the corresponding player object within the game scene.
    /// </summary>
    /// <param name="e">The socket event</param>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public MovePlayer(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.clientId = data["socketId"];
        this.movementType = int.Parse(data["movementType"]);
        this.x = PacketUtils.FromPacketString(data["locationToX"]);
        this.y = PacketUtils.FromPacketString(data["locationToY"]);
        this.z = PacketUtils.FromPacketString(data["locationToZ"]);

        HandlePacket();
    }

    public void HandlePacket() {
        if(!networkManager.IsMain(this.clientId)) {
            Player player = networkManager.GetPlayerFromClientId(clientId);
            Vector3 location = new Vector3(this.x, this.y, this.z);

            playerMoveEvent(player, location, this.movementType);
        }
    }
}
