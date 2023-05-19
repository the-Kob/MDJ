using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Parameters")]
    public float rotationSpeed = 7;
    public float stomachOffset; // Look up and down
    public float lowerVertLimit = -35; // Vertical limit for looking down
    public float upperVertLimit = 20; // Vertical limit for looking up

    [Header("References")]
    public Transform cameraRoot; // What should the camera follow?
    public ConfigurableJoint hipJoint, stomachJoint;

    private float mouseX, mouseY;


    void Start()
    {
        // Make cursor disappear and be locked in window
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        // Get inputs
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;

        // Should be able to look up or down too much
        mouseY = Mathf.Clamp(mouseY, lowerVertLimit, upperVertLimit);

        // Calculate desired camera rotation and apply it to camera root and body parts
        // (so player also rotates the way we're facing)
        Quaternion desiredCameraRotation = Quaternion.Euler(mouseY, mouseX, 0);
        cameraRoot.rotation = desiredCameraRotation;
        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
        stomachJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);
    }
}
