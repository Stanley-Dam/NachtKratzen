/**
 * A simple packet that anounces that the server has been closed and also tells the player why.
 */
class ServerClose {
    constructor(reason) {
        this.reason = reason;
    }
}

module.exports = ServerClose;