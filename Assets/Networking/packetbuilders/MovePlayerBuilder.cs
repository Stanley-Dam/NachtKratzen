using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MovePlayerBuilder : PacketBuilder {

    public MovePlayerBuilder(SocketIO.SocketIOComponent socket, NetworkManager networkManager,
        string socketId, Vector3 posTo, Quaternion rotation) :
        base(socket, networkManager, "PlayerMove") {

        //TODO create a moving packet that is compatible with the packet-protocol
        //https://docs.google.com/document/d/1Y13P_vc6lDv2jMns_a5W37Y1nRPTUWBA6yy2OJYxqzU/edit?usp=sharing

        Send();
    }

}
