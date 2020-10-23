const PropMove = require('../packets/PropMove.js');

/**
 * This class will be created for every moved prop.
 * We can use this data to sync new players and to eventually even make a little anti-cheat :)
 */
class Prop {
    constructor(objectId, isBeingHeld, clientIdFrom, x, y, z, rotationX, rotationY, rotationZ, rotationW) {
        this.objectId = objectId;
        this.isBeingHeld = isBeingHeld;
        this.clientIdFrom = clientIdFrom;
        this.x = x;
        this.y = y;
        this.z = z;
        this.rotationX = rotationX;
        this.rotationY = rotationY;
        this.rotationZ = rotationZ;
        this.rotationW = rotationW;
    }

    /**
     * Creates a move packet from the props current data
     */
    GetPropMovePacket() {
        return new PropMove(this.objectId, this.isBeingHeld, this.clientIdFrom, this.x, this.y, this.z, this.rotationX, this.rotationY, this.rotationZ, this.rotationW);
    }
}

module.exports = Prop;