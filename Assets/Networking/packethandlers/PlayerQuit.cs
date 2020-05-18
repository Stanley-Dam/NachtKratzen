using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class PlayerQuit : PacketHandler, PacketHandlerInterface {

    //Player quit event
    public delegate void PlayerQuitEvent(Player player);
    public static event PlayerQuitEvent playerQuitEvent;

    private Player player;

    /// <summary>
    /// Handles the quit packet mainly by removing the corresponding player object from the scene.
    /// </summary>
    /// <param name="e">The event we need to handle</param>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The networking manager</param>
    public PlayerQuit(SocketIO.SocketIOEvent e, SocketIO.SocketIOComponent socket, NetworkManager networkManager) 
        : base(e, socket, networkManager) {

        this.player = networkManager.GetPlayerFromClientId(data["socketId"]);
        networkManager.RemoveOnlinePlayer(player);

        HandlePacket();
    }

    public void HandlePacket() {
        playerQuitEvent(player);
    }
}
