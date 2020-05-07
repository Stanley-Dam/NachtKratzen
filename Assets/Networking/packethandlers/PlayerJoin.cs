using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class PlayerJoin : PacketHandler, PacketHandlerInterface {

    //Player join event
    public delegate void PlayerJoinEvent(Player player, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId);
    public event PlayerJoinEvent playerJoinEvent;

    private float x;
    private float y;
    private float z;
    private float headRotationX;
    private float headRotationY;
    private float headRotationZ;
    private float headRotationW;
    private string clientId;
    private int playerTypeId;

    /// <summary>
    /// Handles the player join packet, it will spawn a player on the possition given by the server.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="socket"></param>
    /// <param name="networkManager"></param>
    public PlayerJoin(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager) 
        : base(e, socket, networkManager) {

        this.x = PacketUtils.FromPacketString(data["x"]);
        this.y = PacketUtils.FromPacketString(data["y"]);
        this.z = PacketUtils.FromPacketString(data["z"]);

        this.headRotationX = PacketUtils.FromPacketString(data["headRotationX"]);
        this.headRotationY = PacketUtils.FromPacketString(data["headRotationY"]);
        this.headRotationZ = PacketUtils.FromPacketString(data["headRotationZ"]);
        this.headRotationW = PacketUtils.FromPacketString(data["headRotationW"]);

        this.clientId = data["clientId"];
        this.playerTypeId = int.Parse(data["playerType"]);

        HandlePacket();
    }

    public void HandlePacket() {
        //TODO handle join packet by calling the join event
    }
}
