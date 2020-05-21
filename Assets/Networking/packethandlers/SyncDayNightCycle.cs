using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncDayNightCycle : PacketHandler, PacketHandlerInterface {

    //DayNightSync event
    public delegate void DayNightSyncEvent(float time, float secondsPerSecond);
    public static event DayNightSyncEvent dayNightSyncEvent;

    private float time;
    private float secondsPerSecond;

    public SyncDayNightCycle(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.time = PacketUtils.FromPacketString(data["time"]);
        this.secondsPerSecond = PacketUtils.FromPacketString(data["secondsPerSecond"]);

        this.HandlePacket();
    }

    public void HandlePacket() {
        if(dayNightSyncEvent != null)
            dayNightSyncEvent(this.time, this.secondsPerSecond);
    }

}
