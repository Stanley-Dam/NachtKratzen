using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMoveBuilder : PacketBuilder {
    
    public PropMoveBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager, string objectId, bool isBeingHeld, string clientIdFrom, Vector3 positionTo, Quaternion rotationTo) :
        base(socket, networkManager, "PropMove") {

        data["objectId"] = objectId;
        data["isBeingHeld"] = "" + isBeingHeld;
        data["clientIdFrom"] = clientIdFrom;

        data["locationToX"] = PacketUtils.ToPacketString(positionTo.x);
        data["locationToY"] = PacketUtils.ToPacketString(positionTo.y);
        data["locationToZ"] = PacketUtils.ToPacketString(positionTo.z);

        data["rotationToX"] = PacketUtils.ToPacketString(rotationTo.x);
        data["rotationToY"] = PacketUtils.ToPacketString(rotationTo.y);
        data["rotationToZ"] = PacketUtils.ToPacketString(rotationTo.z);
        data["rotationToW"] = PacketUtils.ToPacketString(rotationTo.w);

        Send();
    }

}
