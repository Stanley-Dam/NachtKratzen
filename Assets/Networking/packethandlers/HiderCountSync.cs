using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderCountSync : PacketHandler, PacketHandlerInterface {

    //Hider-count sync event
    public delegate void HiderCountSyncEvent(int hidersLeft);
    public static event HiderCountSyncEvent hiderCountSyncEvent;

    private int hidersLeft;

    public HiderCountSync(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
    : base(e, socket, networkManager) {

        hidersLeft = int.Parse(data["hiderAmount"]);

        HandlePacket();
    }

    public void HandlePacket() {
        if (hiderCountSyncEvent != null)
            hiderCountSyncEvent(this.hidersLeft);
    }
}
