using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LocalJointControl : MonoBehaviour
{
	[Header("Joint Movement Settings")]
	public ConfigurableJoint hipJoint;
	public ConfigurableJoint stomachJoint;
	public float rotationSpeed = 7;
	public float lowerVertLimit = -30; // Vertical limit for looking down
	public float upperVertLimit = 30; // Vertical limit for looking up

	private float mouseX, mouseY;

	// INPUTS 
	private Vector2 lookInput;
	public void OnLook(InputAction.CallbackContext ctx) => lookInput = ctx.ReadValue<Vector2>();

	private void FixedUpdate()
	{
		UpdateJointMovement();
	}

	void UpdateJointMovement()
	{
		// Get inputs
		mouseX += lookInput.x * rotationSpeed * Time.unscaledDeltaTime;
		mouseY += lookInput.y * rotationSpeed * Time.unscaledDeltaTime;

		mouseY = Mathf.Clamp(mouseY, lowerVertLimit, upperVertLimit);

		hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
		stomachJoint.targetRotation = Quaternion.Euler(-mouseY, 0, 0);
	}

}
