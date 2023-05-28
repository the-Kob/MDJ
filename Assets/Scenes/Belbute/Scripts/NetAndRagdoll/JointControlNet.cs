using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class JointControlNet : NetworkBehaviour
{
	[Header("Joint Movement Settings")]
	public ConfigurableJoint hipJoint;
	public ConfigurableJoint stomachJoint;
	public float rotationSpeed = 7;
	public float lowerVertLimit = -30; // Vertical limit for looking down
	public float upperVertLimit = 30; // Vertical limit for looking up

	public InputManager input;

	private float mouseX, mouseY;

	private void FixedUpdate()
	{
		if (!IsOwner) return;

		UpdateJointMovement();
	}

	void UpdateJointMovement()
	{
		// Get inputs
		mouseX += input.lookVector.x * rotationSpeed;
		mouseY += input.lookVector.y * rotationSpeed;

		mouseY = Mathf.Clamp(mouseY, lowerVertLimit, upperVertLimit);

		hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
		stomachJoint.targetRotation = Quaternion.Euler(-mouseY, 0, 0);
	}

}
