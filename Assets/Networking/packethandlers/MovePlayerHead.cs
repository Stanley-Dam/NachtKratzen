using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

class MovePlayerHead : PacketHandler, PacketHandlerInterface {

    //Player move event
    public delegate void PlayerMoveHeadEvent(Player player, Quaternion bodyRotation, Quaternion headRotation);
    public static event PlayerMoveHeadEvent playerMoveHeadEvent;

    private string clientId;

    private float bodyRotX;
    private float bodyRotY;
    private float bodyRotZ;
    private float bodyRotW;

    private float headRotX;
    private float headRotY;
    private float headRotZ;
    private float headRotW;

    /// <summary>
    /// Handles a player movement packet by moving the corresponding player object within the game scene.
    /// </summary>
    /// <param name="e">The socket event</param>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public MovePlayerHead(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.clientId = data["socketId"];

        this.bodyRotX = PacketUtils.FromPacketString(data["bodyRotationX"]);
        this.bodyRotY = PacketUtils.FromPacketString(data["bodyRotationY"]);
        this.bodyRotZ = PacketUtils.FromPacketString(data["bodyRotationZ"]);
        this.bodyRotW = PacketUtils.FromPacketString(data["bodyRotationW"]);

        this.headRotX = PacketUtils.FromPacketString(data["headRotationX"]);
        this.headRotY = PacketUtils.FromPacketString(data["headRotationY"]);
        this.headRotZ = PacketUtils.FromPacketString(data["headRotationZ"]);
        this.headRotW = PacketUtils.FromPacketString(data["headRotationW"]);

        HandlePacket();
    }

    public void HandlePacket() {
        if(!networkManager.IsMain(this.clientId)) {
            Player player = networkManager.GetPlayerFromClientId(clientId);
            Quaternion bodyRotation = new Quaternion(this.bodyRotX, this.bodyRotY, this.bodyRotZ, this.bodyRotW);
            Quaternion headRotation = new Quaternion(this.headRotX, this.headRotY, this.headRotZ, this.headRotW);

            playerMoveHeadEvent(player, bodyRotation, headRotation);
        }
    }
}
