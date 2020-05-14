const Player = require('../obj/Player.js');
const fs = require('fs');

/**
 * Handles join packets.
 * 
 * This function will first put the player on the spawn possition.
 * The server will store this in a player data object.
 * At last this class will send a join packet to all the other player's
 * 
 * @param {The server the player is connecting to} server 
 * @param {The socket received by the server} socket 
 */
function PlayerJoinHandler(server, socket) {
    var content = fs.readFileSync('./data/LobbyLocation.json');
    var spawnPoint = JSON.parse(content);

    var newPlayer = new Player(socket.id, spawnPoint.x, spawnPoint.y, spawnPoint.z, spawnPoint.headRotationX, spawnPoint.headRotationY, spawnPoint.headRotationZ, spawnPoint.headRotationW);

    //We need to send the new player all the other players! Otherwise he will be alone forever!!
    server.connectedPlayers.forEach(player => {
        socket.emit('PlayerJoin', player.GetJoinPacket());
    });

    //When the player is the first one in the server
    if(server.connectedPlayers.length <= 0)
        server.StartGameLoop();

    server.BroadCastToClients('PlayerJoin', newPlayer.GetJoinPacket());
    server.connectedPlayers.push(newPlayer);
    console.log(newPlayer.clientId + " Joined the game (Server: " + server.port + ")");
}

module.exports = PlayerJoinHandler;