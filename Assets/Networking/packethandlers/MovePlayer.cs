using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

class MovePlayer : PacketHandler, PacketHandlerInterface {

    //Player move event
    public delegate void PlayerMoveEvent(Player player, Vector3 destination, Quaternion headRotation);
    public static event PlayerMoveEvent playerMoveEvent;

    private string clientId;
    private float x;
    private float y;
    private float z;
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
    public MovePlayer(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.clientId = data["socketId"];
        this.x = PacketUtils.FromPacketString(data["locationToX"]);
        this.y = PacketUtils.FromPacketString(data["locationToY"]);
        this.z = PacketUtils.FromPacketString(data["locationToZ"]);

        this.headRotX = PacketUtils.FromPacketString(data["headRotationX"]);
        this.headRotY = PacketUtils.FromPacketString(data["headRotationY"]);
        this.headRotZ = PacketUtils.FromPacketString(data["headRotationZ"]);
        this.headRotW = PacketUtils.FromPacketString(data["headRotationW"]);

        HandlePacket();
    }

    public void HandlePacket() {
        if(!networkManager.IsMain(this.clientId)) {
            Player player = networkManager.GetPlayerFromClientId(clientId);
            Vector3 location = new Vector3(this.x, this.y, this.z);
            Quaternion headRotation = new Quaternion(this.headRotX, this.headRotY, this.headRotZ, this.headRotW);

            playerMoveEvent(player, location, headRotation);
        }
    }
}
