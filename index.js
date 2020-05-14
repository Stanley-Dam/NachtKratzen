//Imports
const RedirectPacket = require('./packets/RedirectPacket.js');
const Server = require('./obj/Server.js');

/* 
Mongo DB connection

var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/admin";
var dataBase;
*/

/**
 * Basically matchmaking :)
 * 
 * This is the server every player will connect to by default.
 * The proxy is not "joinable", it holds a list with existing "play-servers".
 * If all these servers are full or none has been created yet it will automaticly create a new server.
 * At last the proxy will send the player a RedirectToServer packet with the port of this new server.
 */
class Proxy {
    constructor() {
        this.servers = [];

        this.io = require('socket.io')({
            transports: ['websocket'],
        });

        this.io.attach(420);
        console.log('listening on *:420');

        this.Run(this);
    }

    Run(proxy) {
        this.io.on('connect', function(socket) {
            socket.on('PlayerJoin', (data) => {
                var joinedExistingGame = false;

                if(proxy.servers.length > 0) {
                    proxy.servers.forEach(server => {
                        if(server.connectedPlayers.length < 20 && !server.gameStarted) {
                            socket.emit('RedirectToServer', new RedirectPacket(server.port));
                            joinedExistingGame = true;
                        }
                    });
                }

                if(!joinedExistingGame) {
                    var newServer = new Server(proxy, 0, (1000 + Math.floor(Math.random() * 8999)));
                    proxy.servers.push(newServer);
                    socket.emit('RedirectToServer', new RedirectPacket(newServer.port));
                }
            });
        }); 
    }

    DestroyServer(server, reason) {
        var index = 0;
        this.servers.forEach(currentServer => {
            if(currentServer == server) {
                console.log(server.port + " has been disabled!");
                this.servers.splice(index);
            }
            index++;
        });

        server.Stop(reason);
    }
}

new Proxy();