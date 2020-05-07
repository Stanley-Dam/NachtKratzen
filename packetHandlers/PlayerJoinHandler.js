const Player = require('../obj/Player.js');
const fs = require('fs')

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
    var content = fs.readFileSync('./data/SpawnLocation.json');
    var spawnPoint = JSON.parse(content);

    var newPlayer = new Player(socket.id, spawnPoint.x, spawnPoint.y, spawnPoint.z, spawnPoint.headRotationX, spawnPoint.headRotationY, spawnPoint.headRotationZ, spawnPoint.headRotationW);
    server.connectedPlayers.push(newPlayer);

    //We need to send the new player all the other players! Otherwise he will be alone forever!!
    server.connectedPlayers.forEach(player => {
        socket.emit('PlayerJoin', player.GetJoinPacket());
    });

    server.BroadCastToClients('PlayerJoin', newPlayer.GetJoinPacket());
    console.log(newPlayer.clientId + " Joined the game (Server: " + server.port + ")");
}

module.exports = PlayerJoinHandler;