using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastPickup : MonoBehaviour {
    public delegate void FoundPlayerEvent(GameObject player);
    public static event FoundPlayerEvent foundPlayerEvent;

    private InputHandler controls;

    [SerializeField] private LocalBodyObjects bodyObjects;

    [SerializeField]
    private float range;

    //[SerializeField]
    //private LineRenderer rayLine;

    [SerializeField]
    private bool isHoldingSomething;

    private void Awake() {
        controls = new InputHandler();
        controls.pickup.Click.performed += ctx => pickUp();
    }

    private void Start()
    {
        //rayLine = GetComponent<LineRenderer>();
    }

    private void pickUp() {
        RaycastHit hit;

        //rayLine.SetPosition(0, cameraPos.position);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)) {
            //rayLine.SetPosition(1, hit.point);
            //when ray collides with an gameobject it moves all parents and children as the grabby hand children
            //call found player event
            if (foundPlayerEvent != null)
                foundPlayerEvent(hit.collider.transform.root.gameObject);
        } else {
            //rayLine.SetPosition(1, cameraPos.position + (cameraPos.transform.forward * range));
        }
    }

    private void OnEnable()
    {
        controls.pickup.Enable();
    }

    private void OnDisable()
    {
        controls.pickup.Disable();
    }
}
