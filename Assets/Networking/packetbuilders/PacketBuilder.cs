using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public abstract class PacketBuilder {

    protected SocketIO.SocketIOComponent socket;
    protected NetworkManager networkManager;
    protected Dictionary<string, string> data;
    private string packetName;

    public PacketBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager, string packetName) {
        this.socket = socket;
        this.networkManager = networkManager;
        this.data = new Dictionary<string, string>();
        this.packetName = packetName;
    }

    protected void Send() {
        socket.Emit(packetName, new JSONObject(data));
    }

}
