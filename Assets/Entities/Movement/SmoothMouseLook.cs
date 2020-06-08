﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Upgraded to this FPC because it's very noice
/// Got it from here: http://wiki.unity3d.com/index.php/SmoothMouseLook?_ga=2.60796269.1704257895.1591006763-1724292300.1577381227
/// </summary>
[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour {

	public delegate void LocalPlayerMoveEvent(Quaternion headRotation);
	public static event LocalPlayerMoveEvent localPlayerHeadMoveEvent;

	public Transform playerBody;
	public Transform playerHead;

	private InputHandler controls;

	[SerializeField] public float sensitivity = 0.1f;

	private Vector2 look = new Vector2();
	private float xRotation = 0f;

	private void Awake() {
		controls = new InputHandler();
	}

	private void OnEnable() {
		controls.movement.Enable();
	}

	private void OnDisable() {
		controls.movement.Disable();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Update() {
		Vector2 oldLook = look;

		look.x = controls.movement.MouseLook.ReadValue<Vector2>().x * sensitivity;
		look.y = controls.movement.MouseLook.ReadValue<Vector2>().y * sensitivity;

		look.x = Mathf.Lerp(oldLook.x, look.x, 1);
		look.y = Mathf.Lerp(oldLook.y, look.y, 1);

		xRotation -= look.y;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
		playerBody.Rotate(Vector3.up * look.x);
		playerHead.localRotation = transform.localRotation;

		if (localPlayerHeadMoveEvent != null)
			localPlayerHeadMoveEvent(playerHead.rotation);
	}

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		transform.localRotation = Quaternion.Euler(0, 0, 0);
	}
}