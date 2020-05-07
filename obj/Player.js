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
}

module.exports = Player;