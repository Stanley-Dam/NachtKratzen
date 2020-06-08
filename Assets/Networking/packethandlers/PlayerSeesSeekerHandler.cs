using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeesSeekerHandler : PacketHandler, PacketHandlerInterface {

    private Seeker seeker;
    private Hider hider;
    private bool isVisible;

    public PlayerSeesSeekerHandler(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager, Seeker seeker)
        : base(e, socket, networkManager) {

        this.seeker = seeker;
        this.hider = (Hider) networkManager.GetPlayerFromClientId(data["socketId"]);
        this.isVisible = bool.Parse(data["isVisible"]);

        HandlePacket();
    }

    public void HandlePacket() {
        this.seeker.PlayerSeesSeekerHandler(this.isVisible, this.hider);
    }
}
