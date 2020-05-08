function PlayerMoveHandler(server, data) {
    var player = server.GetPlayerById(data.socketId);
    
    if(player == null)
        return;

    player.x = data.locationToX;
    player.y = data.locationToY;
    player.z = data.locationToZ;

    player.headRotationX = data.headRotationX;
    player.headRotationY = data.headRotationY;
    player.headRotationZ = data.headRotationZ;
    player.headRotationW = data.headRotationW;

    server.BroadCastToClients('PlayerMove', data);
}

module.exports = PlayerMoveHandler;