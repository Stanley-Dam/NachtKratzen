using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerJoinBuilder: PacketBuilder {

    /// <summary>
    /// A pretty empty packet, it's just our clientId the server will do the rest.
    /// </summary>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public PlayerJoinBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager) : 
        base(socket, networkManager, "PlayerJoin") {

        Send();
    }

}
