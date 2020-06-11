using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeftSync : PacketHandler, PacketHandlerInterface {

    //Time sync event
    public delegate void TimeSyncEvent(int timeLeftInSeconds);
    public static event TimeSyncEvent timeSyncEvent;

    private int timeLeftInSeconds;

    public TimeLeftSync(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
    : base(e, socket, networkManager) {

        timeLeftInSeconds = int.Parse(data["timeLeftInSeconds"]);

        HandlePacket();
    }

    public void HandlePacket() {
        if (timeSyncEvent != null)
            timeSyncEvent(this.timeLeftInSeconds);
    }
}
