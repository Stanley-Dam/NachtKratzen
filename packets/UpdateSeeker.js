/**
 * A very simple packet that tells all the players who's the seeker
 */
class UpdateSeeker {
    constructor(clientId) {
        this.clientId = clientId;
    }
}

module.exports = UpdateSeeker;