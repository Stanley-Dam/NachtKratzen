﻿using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {

    [SerializeField] private SocketIO.SocketIOComponent socket;

    public Player seeker;
    private List<Entity> entities = new List<Entity>();
    private bool joined = false;

    // Start is called before the first frame update
    private void Start() {
        //Start listening to all the server events

        //Connection packets
        socket.On("connect", PlayerJoinServer);
        socket.On("RedirectToServer", Redirect);
        socket.On("ServerClose", ServerClosed);
        socket.On("PlayerJoin", OnPlayerJoin);
        socket.On("PlayerQuit", OnPlayerQuit);
        socket.On("PlayerMove", OnPlayerMove);
        socket.On("PlayerTeleport", OnPlayerTeleport);
        socket.On("UpdateSeeker", OnSeekerUpdate);
        socket.On("PlayerDeath", OnPlayerDeath);

        PlayerMovement.localPlayerMoveEvent += PlayerMoveServer;
    }

    private void OnDisable() {
        PlayerMovement.localPlayerMoveEvent -= PlayerMoveServer;
    }

    /* Send data to the server. */
    public void KillPlayer(string clientId) {
        new PlayerKillBuilder(this.socket, this, clientId);
    }

    private void PlayerJoinServer(SocketIO.SocketIOEvent e) {
        if (!joined)
            new PlayerJoinBuilder(socket, this);
    }

    private void PlayerMoveServer(Vector3 destination, Quaternion headRotation, MovementType movementType) {
        new MovePlayerBuilder(socket, this, socket.sid, destination, headRotation, (int) movementType);
    }

    /* Receive data from the server */

    private void OnPlayerJoin(SocketIO.SocketIOEvent e) {
        new PlayerJoin(e, socket, this);
    }

    private void OnPlayerQuit(SocketIO.SocketIOEvent e) {
        new PlayerQuit(e, socket, this);
    }

    private void OnPlayerMove(SocketIO.SocketIOEvent e) {
        new MovePlayer(e, socket, this);
    }

    private void OnPlayerTeleport(SocketIO.SocketIOEvent e) {
        new TeleportPlayer(e, socket, this);
    }

    private void OnSeekerUpdate(SocketIO.SocketIOEvent e) {
        new UpdateSeeker(e, socket, this);
    }

    private void OnPlayerDeath(SocketIO.SocketIOEvent e) {
        new PlayerDeath(e, socket, this);
    }

    private void ServerClosed(SocketIO.SocketIOEvent e) {
        //This is very temporary ofcourse, just to start a new game real quick :P
        socket.Close();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void Redirect(SocketIO.SocketIOEvent e) {
        string eventAsString = "" + e.data;
        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(eventAsString);

        socket.ReConnect(data["port"]);
        //Start registering new data
        this.Start();
    }

    /* GETTERS & SETTERS
     */

    public void AddOnlinePlayer(Player player) {
        this.entities.Add(player);
    }

    public void RemoveOnlinePlayer(Player player) {
        this.entities.Remove(player);
    }

    public bool IsMain(string clientId) {
        return (this.socket.sid.Equals(clientId));
    }

    public Player GetPlayerFromClientId(string clientId) {
        foreach(Entity entity in this.entities) {
            if (entity is Player && clientId.Equals(entity.ClientId))
                return (Player) entity;
        }

        return null;
    }

    /* Properties
     */

    public bool Joined {
        get { return this.joined; }
        set { this.joined = value; }
    }

    public List<Player> Players {
        get {
            List<Player> playerList = new List<Player>();

            foreach (Entity entity in entities) {
                if (entity is Player)
                    playerList.Add((Player)entity);
            }

            return playerList;
        }
    }
}
