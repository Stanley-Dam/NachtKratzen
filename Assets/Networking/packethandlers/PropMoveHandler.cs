using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMoveHandler : PacketHandler, PacketHandlerInterface {

    private string objectId;
    private bool isBeingHeld;
    private string clientIdFrom;

    private float x;
    private float y;
    private float z;

    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private float rotationW;

    public PropMoveHandler(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.objectId = data["objectId"];
        this.isBeingHeld = bool.Parse(data["isBeingHeld"]);
        this.clientIdFrom = data["clientIdFrom"];

        this.x = PacketUtils.FromPacketString(data["locationToX"]);
        this.y = PacketUtils.FromPacketString(data["locationToY"]);
        this.z = PacketUtils.FromPacketString(data["locationToZ"]);

        this.rotationX = PacketUtils.FromPacketString(data["rotationToX"]);
        this.rotationY = PacketUtils.FromPacketString(data["rotationToY"]);
        this.rotationZ = PacketUtils.FromPacketString(data["rotationToZ"]);
        this.rotationW = PacketUtils.FromPacketString(data["rotationToW"]);

        HandlePacket();
    }

    public void HandlePacket() {
        Prop prop = networkManager.GetPropFromObjectId(objectId);

        Vector3 location = new Vector3(this.x, this.y, this.z);
        Quaternion rotation = new Quaternion(this.rotationX, this.rotationY, this.rotationZ, this.rotationW);

        prop.MovePropFromServer(location, rotation, this.isBeingHeld, this.clientIdFrom);
    }
}
