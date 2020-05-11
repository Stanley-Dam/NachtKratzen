using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    /* This component is suposed to be a part of the player gameobject.
     * It handles all movement input.
     */

    public delegate void LocalPlayerMoveEvent(Vector3 destination, Quaternion headRotation);
    public static event LocalPlayerMoveEvent localPlayerMoveEvent;

    [SerializeField] private LocalBodyObjects localBodyObjects;

    //Thx to brackeys btw, used his tutorial for the input stuff :P
    private CharacterController character;
    private InputHandler controls;
    private Vector2 direction = new Vector2();
    private Vector3 velocity;
    private bool isGrounded = false;
    private bool jumping = false;
    private bool isSprinting = false;

    [SerializeField] private PlayerAudio playerAudio;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float normalSpeed = 2f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float gravity = -9.18f;
    [SerializeField] private float jumpHeight = 2f;

    private void Awake() {
        controls = new InputHandler();
        controls.movement.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.movement.Move.canceled += ctx => direction = new Vector2();
        controls.movement.Jump.performed += ctx => jumping = true;
        controls.movement.Jump.canceled += ctx => jumping = false;
        controls.movement.Sprint.performed += ctx => isSprinting = true;
        controls.movement.Sprint.canceled += ctx => isSprinting = false;

        character = this.GetComponent<CharacterController>();
        //We can't do anything without a rigidbody, so just disable this object when we can't find it :P
        this.enabled = character;
    }

    private void OnEnable() {
        controls.movement.Enable();
    }

    private void OnDisable() {
        controls.movement.Disable();
    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (direction != new Vector2())
            Move();

        if (jumping)
            Jump();

        //Gravity if not grounded
        if(!isGrounded) {
            velocity.y += gravity * Time.deltaTime;
        } else if(velocity.y < -2f) {
            velocity.y = -2f;
        }

        character.Move(velocity * Time.deltaTime);

        localPlayerMoveEvent(transform.position, localBodyObjects.head.rotation);
    }

    private void Move() {
        Vector3 direction3d = new Vector3(direction.x, 0, direction.y);
        direction3d = transform.right * direction3d.x + transform.forward * direction3d.z;

        float speed = normalSpeed;
        if (isSprinting)
            speed = sprintSpeed;

        character.Move(direction3d * speed * Time.deltaTime);

        //Audio
        if (speed == normalSpeed)
            playerAudio.Walk(PlayerAudioType.WALK_AUDIO_CONCRETE);
        else
            playerAudio.Walk(PlayerAudioType.RUN_AUDIO_CONCRETE);
    }

    private void Jump() {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
