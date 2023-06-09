using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public LayerMask Ground;
    public Transform planet;

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        //Vector3 dir = new Vector3(x, y, 0);
        //UpdatePlayerTransform(dir.normalized);

        Attract();
    }

    private void UpdatePlayerTransform(Vector3 movementDirection)
    {
        RaycastHit hitInfo;

        if (GetRaycastDownAtNewPosition(movementDirection, out hitInfo))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, float.PositiveInfinity);

            transform.rotation = finalRotation;
        }
    }

    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit hitInfo)
    {
        Vector3 newPosition = transform.position;
        Ray ray = new Ray(transform.position + movementDirection * 1, -transform.up);

        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, Ground))
        {
            return true;
        }

        return false;
    }

    public void Attract()
    {

        Vector3 gravityUp = (transform.position - planet.position);
        Vector3 localUp = transform.up;
        Debug.DrawLine(transform.position, gravityUp.normalized * 2f, Color.red);


        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
    }
}
