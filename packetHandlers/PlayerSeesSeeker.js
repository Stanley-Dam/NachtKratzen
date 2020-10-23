function PlayerSeesSeeker(server, socket) {
    if(server.seeker != null) {
        server.io.to(server.seeker.clientId).emit('PlayerSeesSeeker', socket);
    }
}

module.exports = PlayerSeesSeeker;