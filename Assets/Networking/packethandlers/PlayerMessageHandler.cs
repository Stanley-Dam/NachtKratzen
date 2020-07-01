using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessageHandler : PacketHandler, PacketHandlerInterface {

    //Player message event
    public delegate void PlayerMessageEvent(MessageTypes messageType, string message);
    public static event PlayerMessageEvent playerMessageEvent;

    private MessageTypes messageType;
    private string message;

    public PlayerMessageHandler(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager)
        : base(e, socket, networkManager) {

        this.messageType = (MessageTypes) Enum.GetValues(typeof(MessageTypes)).GetValue(int.Parse(data["messageType"]));
        this.message = data["message"];

        HandlePacket();
    }

    public void HandlePacket() {
        if (playerMessageEvent != null)
            playerMessageEvent(this.messageType, this.message);
    }
}
