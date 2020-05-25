/**
 * This packet can only be called by the server, it synchronizes the time in all clients.
 */
class SyncDayNightCycle {
    constructor(time, secondsPerSecond) {
        this.time = String(time);
        this.secondsPerSecond = String(secondsPerSecond);
    }
}

module.exports = SyncDayNightCycle;