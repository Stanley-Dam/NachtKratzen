using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHeadBuilder : PacketBuilder {
    /// <summary>
    /// We send this packet every time the main player moves.
    /// </summary>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The networkmanager</param>
    /// <param name="socketId">The socketId of the moving player</param>
    /// <param name="headRotation">The rotation where the player is moving to</param>
    public PlayerMoveHeadBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager,
        string socketId, Quaternion headRotation, Quaternion bodyRotation) :
        base(socket, networkManager, "PlayerMoveHead") {

        data["socketId"] = socketId;

        data["bodyRotationX"] = PacketUtils.ToPacketString(bodyRotation.x);
        data["bodyRotationY"] = PacketUtils.ToPacketString(bodyRotation.y);
        data["bodyRotationZ"] = PacketUtils.ToPacketString(bodyRotation.z);
        data["bodyRotationW"] = PacketUtils.ToPacketString(bodyRotation.w);

        data["headRotationX"] = PacketUtils.ToPacketString(headRotation.x);
        data["headRotationY"] = PacketUtils.ToPacketString(headRotation.y);
        data["headRotationZ"] = PacketUtils.ToPacketString(headRotation.z);
        data["headRotationW"] = PacketUtils.ToPacketString(headRotation.w);

        Send();
    }

}
