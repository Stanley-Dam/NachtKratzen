using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MovePlayerBuilder : PacketBuilder {

    public MovePlayerBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager,
        string socketId, Vector3 posTo, Quaternion rotation) :
        base(socket, networkManager, "PlayerMove") {

        data["socketId"] = socketId;

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
