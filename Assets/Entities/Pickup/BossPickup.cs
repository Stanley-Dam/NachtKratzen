using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastPickup : MonoBehaviour
{
    private InputHandler controls;

    [SerializeField]
    private float range;

    [SerializeField]
    private Transform cameraPos;

    [SerializeField]
    private LineRenderer rayLine;

    [SerializeField]
    private bool isHoldingSomething;

    private void Awake()
    {
        controls = new InputHandler();
        controls.pickup.interact.performed += ctx => pickUp();
    }

    private void Start()
    {
        rayLine = GetComponent<LineRenderer>();
    }

    private void pickUp()
    {
        if (!isHoldingSomething)
        {
            RaycastHit hit;

            rayLine.SetPosition(0, cameraPos.position);

            if (Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, range))
            {
                rayLine.SetPosition(1, hit.point);
            }
            else
            {
                rayLine.SetPosition(1, cameraPos.position + (cameraPos.transform.forward * range));
            }

            print("I am picking "+ hit +" up");
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
