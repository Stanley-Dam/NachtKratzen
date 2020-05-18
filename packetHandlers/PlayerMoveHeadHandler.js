function PlayerMoveHeadHandler(server, data) {
    var player = server.GetPlayerById(data.socketId);
    
    if(player == null)
        return;

    player.headRotationX = data.headRotationX;
    player.headRotationY = data.headRotationY;
    player.headRotationZ = data.headRotationZ;
    player.headRotationW = data.headRotationW;

    server.BroadCastToClients('PlayerMoveHead', data);
}

module.exports = PlayerMoveHeadHandler;