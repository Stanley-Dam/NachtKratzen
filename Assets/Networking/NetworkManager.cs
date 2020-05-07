using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    [SerializeField] private SocketIO.SocketIOComponent socket;

    private List<Entity> entities = new List<Entity>();
    private bool joined = false;

    // Start is called before the first frame update
    void Start() {
        //Start listening to all the server events

        //Connection packets
        socket.On("connect", PlayerJoinServer);
        socket.On("RedirectToServer", Redirect);
    }

    /* Send data to the server. */
    private void PlayerJoinServer(SocketIO.SocketIOEvent e) {
        if (!joined)
            new PlayerJoinBuilder(socket, this);
    }

    /* Receive data from the server */

    private void Redirect(SocketIO.SocketIOEvent e) {
        string eventAsString = "" + e.data;
        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(eventAsString);

        socket.ReConnect(data["port"]);
        //Start registering new data
        this.Start();
    }

    /* GETTERS & SETTERS
     */

    public GameObject GetPlayerFromClientId(string clientId) {
        GameObject foundObject = null;

        foreach(Entity entity in entities) {
            if (entity is Player && entity.ClientId == clientId)
                return entity.gameObject;
        }

        return foundObject;
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
