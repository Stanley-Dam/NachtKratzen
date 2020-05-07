const JoinPacket = require('../packets/JoinPacket.js');

/**
 * This class will be created for every connected player.
 * We can use this data to sync new players and to eventually even make a little anti-cheat :)
 */
class Player {
    constructor(clientId, x, y, z, headRotationX, headRotationY, headRotationZ, headRotationW) {
        this.clientId = clientId;
        this.x = x;
        this.y = y;
        this.z = z;
        this.headRotationX = headRotationX;
        this.headRotationY = headRotationY;
        this.headRotationZ = headRotationZ;
        this.headRotationW = headRotationW;
    }

    /**
     * Creates a join packet from the player's current data
     */
    GetJoinPacket() {
        return new JoinPacket(this.clientId, this.x, this.y, this.z, this.headRotationX, this.headRotationY, this.headRotationZ, this.headRotationW, 0);
    }
}

module.exports = Player;