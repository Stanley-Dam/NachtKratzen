using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public abstract class PacketHandler {

    protected SocketIO.SocketIOEvent e;
    protected Dictionary<string, string> data;
    protected SocketIO.SocketIOComponent socket;
    protected NetworkManager networkManager;

    public PacketHandler(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager) {
        string eventAsString = "" + e.data;
        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(eventAsString);

        this.e = e;
        this.data = data;
        this.socket = socket;
        this.networkManager = networkManager;
    }

}
