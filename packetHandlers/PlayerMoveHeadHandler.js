function PlayerMoveHeadHandler(server, data) {
    var player = server.GetPlayerById(data.socketId);
    
    if(player == null)
        return;

    player.bodyRotationX = data.bodyRotationX;
    player.bodyRotationY = data.bodyRotationY;
    player.bodyRotationZ = data.bodyRotationZ;
    player.bodyRotationW = data.bodyRotationW;

    player.headRotationX = data.headRotationX;
    player.headRotationY = data.headRotationY;
    player.headRotationZ = data.headRotationZ;
    player.headRotationW = data.headRotationW;

    server.BroadCastToClients('PlayerMoveHead', data);
}

module.exports = PlayerMoveHeadHandler;