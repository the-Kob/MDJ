using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Controls")]
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    public float speed; // Forward or backward
    public float strafeSpeed; // Side movement
    public float runFactor = 1.5f;
    public float jumpForce;

    private Rigidbody hips;
    public bool isGrounded;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Move forward and backwards
        if (Input.GetKey(moveForwardKey))
        {
            if (Input.GetKey(runKey))
            {
                hips.AddForce(hips.transform.forward * speed * runFactor);
            }
            else
                hips.AddForce(hips.transform.forward * speed);
        }

        if (Input.GetKey(moveDownKey))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }


        // Move sideways (strafe)
        if (Input.GetKey(moveLeftKey))
        {
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(moveRightKey))
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }

        // Jump
        if(Input.GetAxis("Jump") > 0 || Input.GetKey(jumpKey))
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }

}
