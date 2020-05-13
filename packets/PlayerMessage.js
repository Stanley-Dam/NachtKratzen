/**
 * Will show the player the given message.
 */
class PlayerMessage {
    constructor(messageType, message) {
        this.messageType = messageType;
        this.message = message;

        console.log(message);
    }
}

module.exports = PlayerMessage;