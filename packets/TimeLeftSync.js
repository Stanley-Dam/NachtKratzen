/**
 * A packet that syncs the time left in all player UI's
 */
class TimeLeftSync {
    constructor(timeLeftInSeconds) {
        this.timeLeftInSeconds = timeLeftInSeconds;
    }
}

module.exports = TimeLeftSync;