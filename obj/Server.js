const JoinPacketHandler = require('../packetHandlers/PlayerJoinHandler.js');
const QuitPacketHandler = require('../packetHandlers/PlayerQuitHandler.js');
const MovePacketHandler = require('../packetHandlers/PlayerMoveHandler.js');

/**
 * This is a "playable-server", these get created by the proxy.
 * The proxy also asigns a port to this server.
 * This is bassicly the core of the server, it links to packet-handlers wich will handle all the received packets.
 * The server also contains some handy functions to make handeling and sending packets easier.
 */
class Server {
    constructor(proxy, serverId, port) {
        this.proxy = proxy;
        this.serverId = serverId;
        this.connectedPlayers = [];
        this.port = port;

        this.gameStarted = false;

        this.io = require('socket.io')({
            transports: ['websocket'],
        });

        this.io.attach(this.port);
        console.log("Started listening on: " + this.port);

        //Start updating the server
        this.ServerLoop(this);
    }

    ServerLoop(server) {
        this.io.on('connection', function(socket) {
            socket.on('PlayerJoin', (data) => {
                JoinPacketHandler(server, socket, data);
            });
            
            /* Playerquit & disconnect do the same thing :P
             */

            socket.on('disconnect', function() {
                QuitPacketHandler(server, socket);
            });

            socket.on('PlayerQuit', function() {
                QuitPacketHandler(server, socket);
            });

            socket.on('PlayerMove', function(data) {
                MovePacketHandler(server, data);
            });
        });
    }

    /**
     * A timer for incase you want to delay a certain response for example.
     * @param {The amount of milliseconds the delay has to last.} ms 
     */
    Sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    /**
     * Stops this server.
     */
    Stop() {
        this.io.close();
    }

    GetPlayerById(clientId) {

        for(var i = 0; i < this.connectedPlayers.length; i++) {
            if(this.connectedPlayers[i].clientId == clientId)
                return this.connectedPlayers[i];
        }
    }

    /**
     * Broadcasts this packet to all the connected clients, this packet will have to be compatible with the Packet-protocol
     * https://docs.google.com/document/d/1Y13P_vc6lDv2jMns_a5W37Y1nRPTUWBA6yy2OJYxqzU/edit?usp=sharing
     * 
     * @param {The packet's name} pakcetName 
     * @param {The data send with this packet} data 
     */
    BroadCastToClients(packetName, data) {
        this.io.sockets.clients().emit(packetName, data);
    }

    /**
     * This sends a packet without any extra data.
     * @param {The packet's name} packetName 
     */
    async MessageToClients(packetName) {
        this.io.sockets.clients().emit(packetName);
        return;
    }
}

module.exports = Server;