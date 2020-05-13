using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossPickup : MonoBehaviour
{
    public InputHandler controls;

    [SerializeField]
    private float range;

    private void Awake()
    {
        controls = new InputHandler();
        controls.pickup.interact.performed += ctx => pickUp();
    }

    private void pickUp()
    {
        print("ok");
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
