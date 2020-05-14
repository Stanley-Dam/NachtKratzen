using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    /* Simple class to controll the player camera, has to be on the camera.
     */

    //These are public so that the player can eventually change them in their settings.
    public float mouseSensitivity = 7f;

    private Vector2 look = new Vector2();
    private float xRotation = 0f;
    private InputHandler controls;

    public Transform playerBody;
    public Transform playerHead;

    private void Awake() {
        controls = new InputHandler();
    }

    private void OnEnable() {
        controls.movement.Enable();
    }

    private void OnDisable() {
        controls.movement.Disable();
    }

    private void Start() {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        look.x = controls.movement.MouseLook.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        look.y = controls.movement.MouseLook.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= look.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * look.x);
        playerHead.localRotation = transform.localRotation;
    }

}
