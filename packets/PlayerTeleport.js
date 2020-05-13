/**
 * Will teleport the player to the specified location.
 */
class PlayerTeleport {
    constructor(clientId, locationToX, locationToY, locationToZ, headRotationX, headRotationY, headRotationZ, headRotationW) {
        this.clientId = clientId;
        
        this.locationToX = locationToX;
        this.locationToY = locationToY;
        this.locationToZ = locationToZ;

        this.headRotationX = headRotationX;
        this.headRotationY = headRotationY;
        this.headRotationZ = headRotationZ;
        this.headRotationW = headRotationW;
    }
}

module.exports = PlayerTeleport;