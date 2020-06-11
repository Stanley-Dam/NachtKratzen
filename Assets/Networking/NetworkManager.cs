using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {

    [SerializeField] private SocketIO.SocketIOComponent socket;
    [SerializeField] private TimeScript timeScript;

    public Player seeker;
    private List<Entity> entities = new List<Entity>();
    private bool joined = false;

    private static NetworkManager networkManager;
    public static NetworkManager Instance { get { return networkManager; } }

    private void Awake() {
        if (networkManager == null)
            networkManager = this;
        else
            Destroy(this);
    }

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
        socket.On("PlayerMoveHead", OnPlayerMoveHead);
        socket.On("PlayerTeleport", OnPlayerTeleport);
        socket.On("UpdateSeeker", OnSeekerUpdate);
        socket.On("PlayerDeath", OnPlayerDeath);
        socket.On("PropMove", OnPropMove);

        socket.On("TimeLeftSync", OnTimeLeftSync);
        socket.On("PlayerMessage", OnPlayerMessage);
        socket.On("PlayerSeesSeeker", OnPlayerSeesSeeker);
        socket.On("HiderCountSync", OnHiderCountSync);

        socket.On("SyncDayNightCycle", OnSyncDayNightCycle);
        socket.On("StartDayNightCycle", OnStartDayNightCycle);
        socket.On("StopDayNightCycle", OnStopDayNightCycle);

        PlayerMovement.localPlayerMoveEvent += PlayerMoveServer;
        SmoothMouseLook.localPlayerHeadMoveEvent += PlayerMoveHeadServer;
    }

    private void OnDisable() {
        PlayerMovement.localPlayerMoveEvent -= PlayerMoveServer;
        SmoothMouseLook.localPlayerHeadMoveEvent -= PlayerMoveHeadServer;
    }

    /* Send data to the server. */
    public void KillPlayer(string clientId) {
        new PlayerKillBuilder(this.socket, this, clientId);
    }

    public void PlayerSeesSeeker(bool isSeekerVisible) {
        new PlayerSeesSeekerBuilder(this.socket, this, isSeekerVisible);
    }

    public void MoveProp(Prop prop, Vector3 positionTo, Quaternion rotationTo) {
        new PropMoveBuilder(socket, this, prop.ObjectId, prop.isPickedUp, socket.sid, positionTo, rotationTo);
    }

    private void PlayerJoinServer(SocketIO.SocketIOEvent e) {
        if (!joined)
            new PlayerJoinBuilder(socket, this);
    }

    private void PlayerMoveServer(Vector3 destination, MovementType movementType) {
        new MovePlayerBuilder(socket, this, socket.sid, destination, (int) movementType);
    }

    private void PlayerMoveHeadServer(Quaternion headRotation, Quaternion bodyRotation) {
        new PlayerMoveHeadBuilder(socket, this, socket.sid, headRotation, bodyRotation);
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

    private void OnPlayerMoveHead(SocketIO.SocketIOEvent e) {
        new MovePlayerHead(e, socket, this);
    }

    private void OnSyncDayNightCycle(SocketIO.SocketIOEvent e) {
        new SyncDayNightCycle(e, socket, this);
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

    private void OnHiderCountSync(SocketIO.SocketIOEvent e) {
        new HiderCountSync(e, socket, this);
    }

    private void OnTimeLeftSync(SocketIO.SocketIOEvent e) {
        new TimeLeftSync(e, socket, this);
    }

    private void OnPlayerMessage(SocketIO.SocketIOEvent e) {
        new PlayerMessageHandler(e, socket, this);
    }

    private void OnPlayerSeesSeeker(SocketIO.SocketIOEvent e) {
        new PlayerSeesSeekerHandler(e, socket, this, (Seeker) this.seeker);
    }

    private void OnPropMove(SocketIO.SocketIOEvent e) {
        new PropMoveHandler(e, socket, this);
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

    /* Handeling simple server messages. (packets without additional data)
     */

    private void OnStartDayNightCycle(SocketIO.SocketIOEvent e) {
        timeScript.doingCycle = true;
    }

    private void OnStopDayNightCycle(SocketIO.SocketIOEvent e) {
        timeScript.doingCycle = false;
    }

    /* GETTERS & SETTERS
     */
    
    public void AddProp(Prop prop) {
        this.entities.Add(prop);
    }

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
            if (entity is Player && clientId.Equals(((Player) entity).ClientId))
                return (Player) entity;
        }

        return null;
    }

    public Prop GetPropFromObjectId(string objectId) {
        foreach(Entity entity in this.entities) {
            if (entity is Prop && objectId.Equals(((Prop)entity).ObjectId))
                return (Prop) entity;
        }

        return null;
    }

    /* Properties
     */

    public SocketIO.SocketIOComponent Socket {
        get { return this.socket; }
    }

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
