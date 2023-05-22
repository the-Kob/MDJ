using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointControl : MonoBehaviour
{
	[Header("Joint Movement Settings")]
	public ConfigurableJoint hipJoint;
	public ConfigurableJoint stomachJoint;
	public float rotationSpeed = 7;
	public float lowerVertLimit = -30; // Vertical limit for looking down
	public float upperVertLimit = 30; // Vertical limit for looking up

	public Camera cam;

	private float mouseX, mouseY;

	private void FixedUpdate()
	{
		UpdateJointMovement();
	}

	void UpdateJointMovement()
	{
		// Get inputs
		mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
		mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;

		mouseY = Mathf.Clamp(mouseY, lowerVertLimit, upperVertLimit);

		hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
		stomachJoint.targetRotation = Quaternion.Euler(-mouseY, 0, 0);

	}

}
