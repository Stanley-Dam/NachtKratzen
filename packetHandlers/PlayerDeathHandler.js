function PlayerDeathHandler(server, socket) {
    var player = server.GetPlayerById(socket.clientId);

    if(server.gameStarted) {
        if(server.seeker == player)
            server.seeker = null;

        if(server.hiders.includes(player)) {
            server.hiders.splice(server.hiders.indexOf(player), 1);
        }
    }

    server.BroadCastToClients('PlayerDeath', socket);
    console.log(player.clientId + " Died (Server: " + server.port + ")");
}

module.exports = PlayerDeathHandler;