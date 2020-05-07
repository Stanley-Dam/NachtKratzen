/**
 * Bassicly puts all the join packet data in JSON
 */
class JoinPacket {
    constructor(socketId, spawnLocationX, spawnLocationY, spawnLocationZ, headRotationX, headRotationY, headRotationZ, headRotationW, playerType) {
        this.socketId = socketId;

        this.spawnLocationX = spawnLocationX;
        this.spawnLocationY = spawnLocationY;
        this.spawnLocationZ = spawnLocationZ;

        this.headRotationX = headRotationX;
        this.headRotationY = headRotationY;
        this.headRotationZ = headRotationZ;
        this.headRotationW = headRotationW;

        this.playerType = playerType;
    }
}

module.exports = JoinPacket;