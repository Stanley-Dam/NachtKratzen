using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MovePlayerBuilder : PacketBuilder {

    /// <summary>
    /// We send this packet every time the main player moves.
    /// </summary>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The networkmanager</param>
    /// <param name="socketId">The socketId of the moving player</param>
    /// <param name="posTo">The position where the player is moving to</param>
    /// <param name="rotation">The head-rotation of the player</param>
    public MovePlayerBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager,
        string socketId, Vector3 posTo, Quaternion rotation, int movementType) :
        base(socket, networkManager, "PlayerMove") {

        data["socketId"] = socketId;
        data["movementType"] = movementType.ToString();

        data["locationToX"] = PacketUtils.ToPacketString(posTo.x);
        data["locationToY"] = PacketUtils.ToPacketString(posTo.y);
        data["locationToZ"] = PacketUtils.ToPacketString(posTo.z);

        data["headRotationX"] = PacketUtils.ToPacketString(rotation.x);
        data["headRotationY"] = PacketUtils.ToPacketString(rotation.y);
        data["headRotationZ"] = PacketUtils.ToPacketString(rotation.z);
        data["headRotationW"] = PacketUtils.ToPacketString(rotation.w);

        Send();
    }

}
