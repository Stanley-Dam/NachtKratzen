using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerSeesSeeker : PacketBuilder {

    /// <summary>
    /// A pretty empty packet, it's just our clientId the server will do the rest.
    /// </summary>
    /// <param name="socket">The socket</param>
    /// <param name="networkManager">The network manager</param>
    public PlayerSeesSeeker(SocketIO.SocketIOComponent socket, NetworkManager networkManager, bool isSeekerVisible) : 
        base(socket, networkManager, "PlayerSeesSeeker") {

        data["socketId"] = networkManager.Socket.sid;
        data["isVisible"] = isSeekerVisible.ToString();

        Send();
    }

}
