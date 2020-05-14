const QuitPacket = require('../packets/QuitPacket.js');

/**
 * Removes the player from the player-list and updates all clients with a QuitPacket
 * @param {The server that has been left} server 
 * @param {The socket received} socket 
 */
function PlayerQuitHandler(server, socket) {
    var player = server.GetPlayerById(socket.id);
    var index = server.connectedPlayers.indexOf(player);
    server.connectedPlayers.splice(index, 1);

    if(server.gameStarted) {
        if(server.seeker == player)
            server.seeker = null;

        if(server.hiders.includes(player)) {
            server.hiders.splice(server.hiders.indexOf(player), 1);
        }
    }

    var dataObject = new QuitPacket(socket.id);
            
    server.BroadCastToClients('PlayerQuit', dataObject);
    console.log(dataObject.socketId + " left the game (Server: " + server.port + ")");
}

module.exports = PlayerQuitHandler;