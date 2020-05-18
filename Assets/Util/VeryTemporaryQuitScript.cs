using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VeryTemporaryQuitScript : MonoBehaviour {

    private InputHandler inputHandler;

    // Start is called before the first frame update
    private void Awake() {
        inputHandler = new InputHandler();
        inputHandler.movement.Quit.performed += ctx => Quit();
    }

    private void OnEnable() {
        inputHandler.movement.Enable();
    }

    private void OnDisable() {
        inputHandler.movement.Disable();
    }

    private void Quit() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
