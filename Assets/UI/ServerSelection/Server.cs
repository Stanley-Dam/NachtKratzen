using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Server : MonoBehaviour {

    [SerializeField] private Text text;

    private string serverName = "Nachtkratzen.net";
    private string serverIp = "93.104.208.167";
    private string port = "420";

    private void Awake() {
        text.text = serverName;
    }

    public void ServerJoin() {
        GameManager.ip = serverIp;
        GameManager.port = port;

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
