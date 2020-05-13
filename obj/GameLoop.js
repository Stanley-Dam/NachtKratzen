const fs = require('fs');

const langFileContent = fs.readFileSync('./data/Lang.json');
const Language = JSON.parse(langFileContent);

const spawnLocationFile = fs.readFileSync('./data/SpawnLocation.json');
const spawnPoint = JSON.parse(spawnLocationFile);

const PlayerMessage = require('../packets/PlayerMessage.js');
const PlayerTeleport = require('../packets/PlayerTeleport.js');
const UpdateSeeker = require('../packets/UpdateSeeker.js');

const Stages = require('../data/Stages.js');
const MessageTypes = require('../data/MessageTypes.js');

const TICK_RATE = 20;

/**
 * The gameloop is here so we can do server sided game updates :)
 */
class GameLoop {
    constructor(server) {
        this.server = server;

        this.tick = 0;
        this.clock = 0;
        this.previous = this.hrtimeMs();
        this.tickLengthMs = 1000 / TICK_RATE;

        this.gameStage = Stages.WAITING_FOR_PLAYERS;

        setTimeout(this.Loop, this.tickLengthMs);
    }

    //Basically the actual gameloop :P
    Loop = () => {
        if(this.server.isEnabled) {
            setTimeout(this.Loop, this.tickLengthMs);
            let now = this.hrtimeMs();
            let delta = (now - this.previous) / 1000;
            this.Update(delta);
            this.previous = now;
            this.tick++;
        }
    }

    hrtimeMs() {
        let time = process.hrtime();
        return time[0] * 1000 + time[1] / 1000000;
    }

    //Gets called about 20 times per second
    Update(deltaTime) {
        this.clock += deltaTime;

        /**
         * @see https://docs.google.com/document/d/1B37fRN4JlXPEVjBk-zfjm9Jxi7HfkaBcQ8tgQnmvBxM/edit
         * for more information about the different game stages and the timings between them.
         */
        switch(this.gameStage) {
            case Stages.WAITING_FOR_PLAYERS:
                if(this.CheckForPlayersOnline() > 3) {
                    this.gameStage = Stages.STARTING;
                    this.clock = 0;
                }
                break;
            case Stages.STARTING:
                if(this.CheckForPlayersOnline() < 3) {
                    this.clock = 0;
                    this.gameStage = Stages.WAITING_FOR_PLAYERS;
                    //Let's also give the players a heads up :)
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.SAD_NEWS, Language.PLAYER_LEFT_SERVER_CANCELED));
                    break;
                }

                var timeLeft = 30 - this.clock;

                if(timeLeft <= 0) {
                    //Let's start the actual game! :D
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.HAPPY_NEWS, Language.GAME_STARTING));
                    this.gameStage = Stages.HIDE_TIME;
                    this.clock = 0;
                    this.server.gameStarted = true;
                    this.PickSeeker();
                    this.TeleportPlayers(0);
                    break;
                }

                this.BroadCastTimeMilestone(timeLeft);
                break;
            case Stages.HIDE_TIME:
                //Count down 30 seconds, check if seeker is still online
                if(this.server.seeker == null) {
                    this.clock = 0;
                    this.gameStage = Stages.WAITING_FOR_PLAYERS;
                    //Let's also give the players a heads up :)
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.SAD_NEWS, Language.PLAYER_LEFT_SERVER_CANCELED));
                    break;
                }

                var timeLeft = 30 - this.clock;

                if(timeLeft <= 0) {
                    //Here we release the seeker!
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.SPOOKY_NEWS, Language.SEEKER_RELEASED));
                    this.gameStage = Stages.INGAME;
                    this.clock = 0;
                    this.TeleportPlayers(1);
                    break;
                }

                this.BroadCastTimeMilestone(timeLeft);

                break;
            case Stages.INGAME:
                //Can last 5 minutes at maximum
                var timeLeft = 300 - this.clock;

                if(this.server.seeker == null || timeLeft <= 0) {
                    //Hiders have won the game!
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.HAPPY_NEWS, Language.HIDERS_WIN));
                    this.gameStage = Stages.END;
                    this.clock = 0;
                    break;
                }

                if(this.server.hiders.length <= 0) {
                    //The seeker has won the game!
                    this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.SPOOKY_NEWS, Language.SEEKER_WINS));
                    this.gameStage = Stages.END;
                    this.clock = 0;
                    break;
                }
                break;
            case Stages.END:
                var timeLeft = 20 - this.clock;
                //TODO Send stuff for end screen? :)

                if(timeLeft <= 0) {
                    this.server.proxy.DestroyServer(this.server, "Game ended");
                }

                break;
        }
    }

    /**
     * This function will disable the server if no one is online :)
     * @returns {the amount of connected players} amount
     */
    CheckForPlayersOnline() {
        var amount = this.server.connectedPlayers.length;

        if(amount <= 0)
            this.server.proxy.DestroyServer(this.server, "Not enough players");

        return amount;
    }

    /**
     * This will broadcast a message to all the online players every 10 seconds :)
     * @param {The time left} time 
     */
    BroadCastTimeMilestone(time) {
        var flooredTime = Math.floor(time);
        var doBroadCast = false;

        if(flooredTime % 10 == 0 || flooredTime < 5)
            doBroadCast = true;

        if(doBroadCast)
            this.server.BroadCastToClients('PlayerMessage', new PlayerMessage(MessageTypes.HAPPY_NEWS, flooredTime + Language.SECONDS_LEFT));
    }

    /**
     * Picks a random seeker from the online player list.
     */
    PickSeeker() {
        var pickedIndex = Math.floor(Math.random() * this.server.connectedPlayers.length);
        this.server.seeker = this.server.connectedPlayers[pickedIndex];
        this.server.BroadCastToClients('UpdateSeeker', new UpdateSeeker(this.server.seeker.clientId));

        //Setting up the hider array
        server.connectedPlayers.forEach(player => {
            if(player != this.server.seeker)
                this.server.hiders.push(player);
        });
    }

    /**
     * Teleports a group of players to the playing map's spawn-point
     * @param {Has to be either 0 for hiders or 1 for the seeker} type 
     */
    TeleportPlayers(type) {
        if(type == 1) {
            this.TeleportPlayerToSpawn(this.server.seeker);
            return;
        }

        this.server.hiders.forEach(player => {
            this.TeleportPlayerToSpawn(player);
        });
    }

    TeleportPlayerToSpawn(player) {
        this.server.BroadCastToClients('PlayerTeleport', new PlayerTeleport(player.clientId, 
            spawnPoint.x, 
            spawnPoint.y, 
            spawnPoint.z, 
            spawnPoint.headRotationX, 
            spawnPoint.headRotationY, 
            spawnPoint.headRotationZ, 
            spawnPoint.headRotationW));
    }
}

module.exports = GameLoop;