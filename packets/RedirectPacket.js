/**
 * A very simple packet that redirects a player to a "playable server"
 */
class RedirectPacket {
    constructor(port) {
        this.port = port;
    }
}

module.exports = RedirectPacket;