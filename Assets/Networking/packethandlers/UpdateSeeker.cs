using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSeeker : PacketHandler, PacketHandlerInterface {

    //Seeker update event
    public delegate void UpdateSeekerEvent(Player player);
    public static event UpdateSeekerEvent updateSeekerEvent;

    private Player player;

    public UpdateSeeker(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
    : base(e, socket, networkManager) {
        this.player = networkManager.GetPlayerFromClientId(data["clientId"]);
        this.HandlePacket();
    }

    public void HandlePacket() {
        updateSeekerEvent(this.player);
    }
}
