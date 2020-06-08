using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour {

    /* This component is suposed to be a part of the player gameobject.
     * It handles all movement input.
     */

    public delegate void LocalPlayerMoveEvent(Vector3 destination, MovementType movementType);
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
    private bool isCrouching = false;
    private MovementType movementType = MovementType.IDLE;
    private Vector3 positionLastFrame = new Vector3(0, 0);

    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private PlayerTypes playerType;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 2.5f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float normalSpeed = 20f;
    [SerializeField] private float sprintSpeed = 50f;
    [SerializeField] private float gravity = -90f;
    [SerializeField] private float jumpHeight = 12f;
    [SerializeField] private float crouchCameraShift = 0.4f;

    private void Awake() {
        controls = new InputHandler();
        controls.movement.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.movement.Move.canceled += ctx => direction = new Vector2();
        controls.movement.Jump.performed += ctx => jumping = true;
        controls.movement.Jump.canceled += ctx => jumping = false;
        controls.movement.Sprint.performed += ctx => isSprinting = true;
        controls.movement.Sprint.canceled += ctx => isSprinting = false;

        controls.movement.Crouch.performed += ctx => {
            isCrouching = true;

            //Shift the camera down a bit for good player feedback
            localBodyObjects.cameraHolder.localPosition += new Vector3(0, -crouchCameraShift);
        };

        controls.movement.Crouch.canceled += ctx => {
            isCrouching = false;

            //Set the camera back to it's normal position
            localBodyObjects.cameraHolder.localPosition += new Vector3(0, crouchCameraShift);
        };

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

        if(positionLastFrame != this.transform.position) {
            positionLastFrame = this.transform.position;
            if (localPlayerMoveEvent != null)
                localPlayerMoveEvent(transform.position, movementType);
        } else if(movementType != MovementType.IDLE) {
            movementType = MovementType.IDLE;
            if(localPlayerMoveEvent != null)
                localPlayerMoveEvent(transform.position, movementType);
        }
    }

    private void Move() {
        Vector3 direction3d = new Vector3(direction.x, 0, direction.y);
        direction3d = transform.right * direction3d.x + transform.forward * direction3d.z;

        float speed = normalSpeed;

        if (isCrouching) {
            speed = crouchSpeed;
        } else if (isSprinting) {
            speed = sprintSpeed;
        }

        character.Move(direction3d * speed * Time.deltaTime);

        //Audio
        if (speed == normalSpeed) {
            movementType = MovementType.WALKING;
            playerAudio.Walk(PlayerAudioType.GetWalkByPlayerType(this.playerType));
        } else if(isSprinting) {
            movementType = MovementType.RUNNING;
            playerAudio.Walk(PlayerAudioType.GetRunByPlayerType(this.playerType));
        }

        //We don't need to handle player crouching sound because you don't make any sound when crouching ;)
    }

    private void Jump() {
        if (isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            movementType = MovementType.JUMPING;
            playerAudio.Jump(PlayerAudioType.GetJumpByPlayerType(this.playerType));
        }
    }
}
