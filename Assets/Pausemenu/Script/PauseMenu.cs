using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputHandler controls;

    [SerializeField]
    private GameObject pauseMenu, pauseMainMenu, optionsMenu;

    [SerializeField]
    private bool paused, inOptions = false;

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

    void Pause()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        if (paused)
        {
            //Disable movement
            //Disable mouselook
            //Show mouse
        }
        else if (!paused)
        {
            //Enable movement
            //Enable mouselook
            //hide mouse
        }
    }

    public void Options()
    {
        inOptions = !inOptions;
        pauseMainMenu.SetActive(!inOptions);
        optionsMenu.SetActive(inOptions);
    }

    public void LeaveGame()
    {
        //call leave game function
    }
    
}
