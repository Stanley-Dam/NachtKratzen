using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastPickup : MonoBehaviour
{
    public delegate void FoundPlayerEvent(GameObject player);
    public static event FoundPlayerEvent foundPlayerEvent;

    private InputHandler controls;

    [SerializeField]
    private float range;

    private Transform cameraPos;

    //[SerializeField]
    //private LineRenderer rayLine;

    [SerializeField]
    private bool isHoldingSomething;

    private void Awake()
    {
        controls = new InputHandler();
        controls.pickup.interact.performed += ctx => pickUp();
        if(Camera.main != null)
        cameraPos = Camera.main.transform;
    }

    private void Start()
    {
        //rayLine = GetComponent<LineRenderer>();
    }

    private void pickUp()
    {
        if (!isHoldingSomething)
        {
            RaycastHit hit;

            //rayLine.SetPosition(0, cameraPos.position);

            if (Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, range))
            {
                //rayLine.SetPosition(1, hit.point);
                //when ray collides with an gameobject it moves all parents and children as the grabby hand children
                //call found player event
                if (foundPlayerEvent != null)
                    foundPlayerEvent(hit.collider.gameObject);
            }
            else
            {
                //rayLine.SetPosition(1, cameraPos.position + (cameraPos.transform.forward * range));
            }

            //print("I am picking "+ hit +" up");
        }
        else if (isHoldingSomething)
        {
            //let go
        }
        //error catch if bool = null
        else
        {
            return;
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
