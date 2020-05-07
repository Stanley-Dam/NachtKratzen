/**
 * A very simple packet to notify a player has left the game
 */
class QuitPacket {
    constructor(socketId) {
        this.socketId = socketId;
    }
}

module.exports = QuitPacket;