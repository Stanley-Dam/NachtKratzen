using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public delegate void PauseEvent(bool pause);
    public static event PauseEvent pauseEvent;

    private InputHandler controls;

    [SerializeField]
    private GameObject pauseMenu, pauseMainMenu, optionsMenu;

    [SerializeField]
    private bool paused, inOptions = false;

    [SerializeField] private NetworkManager networkManager;

    private void Awake()
    {
        controls = new InputHandler();
        controls.keyboard.Pause.performed += ctx => Pause();
    }

    private void OnEnable()
    {
        controls.keyboard.Enable();
    }

    private void OnDisable()
    {
        controls.keyboard.Disable();
    }

    public void Pause() {
        paused = !paused;
        pauseMenu.SetActive(paused);

        if (pauseEvent != null)
            pauseEvent(paused);
    }

    public void Options() {
        inOptions = !inOptions;
        pauseMainMenu.SetActive(!inOptions);
        optionsMenu.SetActive(inOptions);
    }

    public void LeaveGame() {
        networkManager.Socket.Close();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    
}
