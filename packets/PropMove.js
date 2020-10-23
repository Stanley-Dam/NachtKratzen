/**
 * Will move a prop to the specified location.
 */
class PropMove {
    constructor(objectId, isBeingHeld, clientIdFrom, x, y, z, rotationX, rotationY, rotationZ, rotationW) {
        this.objectId = objectId;
        this.isBeingHeld = isBeingHeld;
        this.clientIdFrom = clientIdFrom;
        this.locationToX = x;
        this.locationToY = y;
        this.locationToZ = z;
        this.rotationToX = rotationX;
        this.rotationToY = rotationY;
        this.rotationToZ = rotationZ;
        this.rotationToW = rotationW;
    }
}

module.exports = PropMove;