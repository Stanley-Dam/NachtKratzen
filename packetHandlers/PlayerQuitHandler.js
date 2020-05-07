const QuitPacket = require('../packets/QuitPacket.js');

/**
 * Removes the player from the player-list and updates all clients with a QuitPacket
 * @param {The server that has been left} server 
 * @param {The socket received} socket 
 */
function PlayerQuitHandler(server, socket) {
    var index = 0;
    server.connectedPlayers.forEach(player => {
        if(player.clientId == socket.id) {
            server.connectedPlayers.splice(index);
        }
        index++;
    });

    var dataObject = new QuitPacket(socket.id);
            
    server.BroadCastToClients('PlayerQuit', dataObject);
    console.log(dataObject.socketId + " left the game (Server: " + server.port + ")");
}

module.exports = PlayerQuitHandler;