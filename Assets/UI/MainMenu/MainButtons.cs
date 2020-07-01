using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour {

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject serverSelection;

    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private Quaternion mainMenuRotation;
    [SerializeField] private Quaternion settingsRotation;
    [SerializeField] private Quaternion serverMenuRotation;

    private bool isRotating = false;
    private Quaternion rotationTo;
    private GameObject currentMenu;
    private GameObject menuTo;

    private void Awake() {
        rotationTo = mainMenuRotation;
        currentMenu = mainMenu;
    }

    private void Update() {
        if(isRotating) {
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rotationTo, rotationSpeed * Time.deltaTime);

            if(Camera.main.transform.rotation == rotationTo) {
                isRotating = false;
                UpdateCurrentMenu();
            }
        }
    }

    private void UpdateCurrentMenu() {
        menuTo.SetActive(true);
        currentMenu = menuTo;
    }

    public void ServerMenu() {
        currentMenu.SetActive(false);
        rotationTo = serverMenuRotation;
        menuTo = serverSelection;
        isRotating = true;
    }

    public void Settings() {
        currentMenu.SetActive(false);
        rotationTo = settingsRotation;
        menuTo = settingsMenu;
        isRotating = true;
    }

    public void Back() {
        currentMenu.SetActive(false);
        rotationTo = mainMenuRotation;
        menuTo = mainMenu;
        isRotating = true;
    }

    public void Quit() {
        Application.Quit();
    }

}
