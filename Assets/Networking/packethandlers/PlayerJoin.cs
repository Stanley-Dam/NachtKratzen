using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerJoin : PacketHandler, PacketHandlerInterface {

    //Player join event
    public delegate void PlayerJoinEvent(string clientId, Vector3 spawnLocation, Quaternion headRotation, int playerTypeId, bool isMain);
    public static event PlayerJoinEvent playerJoinEvent;

    private float x;
    private float y;
    private float z;
    private float headRotationX;
    private float headRotationY;
    private float headRotationZ;
    private float headRotationW;
    private string clientId;
    private int playerTypeId;
    private bool isMain;

    /// <summary>
    /// Handles the player join packet, it will spawn a player on the possition given by the server.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="socket"></param>
    /// <param name="networkManager"></param>
    public PlayerJoin(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager) 
        : base(e, socket, networkManager) {

        this.clientId = data["socketId"];

        this.x = PacketUtils.FromPacketString(data["spawnLocationX"]);
        this.y = PacketUtils.FromPacketString(data["spawnLocationY"]);
        this.z = PacketUtils.FromPacketString(data["spawnLocationZ"]);

        this.headRotationX = PacketUtils.FromPacketString(data["headRotationX"]);
        this.headRotationY = PacketUtils.FromPacketString(data["headRotationY"]);
        this.headRotationZ = PacketUtils.FromPacketString(data["headRotationZ"]);
        this.headRotationW = PacketUtils.FromPacketString(data["headRotationW"]);

        this.playerTypeId = int.Parse(data["playerType"]);
        this.isMain = networkManager.IsMain(this.clientId);

        HandlePacket();
    }

    public void HandlePacket() {
        Vector3 position = new Vector3(this.x, this.y, this.z);
        Quaternion headRotation = new Quaternion(this.headRotationX, this.headRotationY, this.headRotationZ, this.headRotationW);
        playerJoinEvent(this.clientId, position, headRotation, this.playerTypeId, this.isMain);
    }
}
