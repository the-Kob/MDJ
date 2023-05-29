using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Grab : MonoBehaviour
{
    [Header("Grab Settings")]
    public KeyCode grabKey;
    public bool isRightHand;
    public int objectMassReduction = 2;

    [Header("References")]
    public Animator animator;


    // Auxiliary variables
    private bool isGrabbing;
    private float originalObjectMass;
    private Rigidbody objectsRB;



    void Update()
    {

        if (InputManager.Instance.GetGrabRightFlag())
        {
            animator.SetBool("IsRightHandUp", true);

            isGrabbing = true;
        }
        else
        {
            animator.SetBool("IsRightHandUp", false);

            // Reset object's mass...
            if (objectsRB != null)
            {
                objectsRB.mass = originalObjectMass;
                objectsRB = null;
            }

            // ...and eliminate fixed joint
            Destroy(GetComponent<FixedJoint>());

            isGrabbing = false;
        }

        if (InputManager.Instance.GetGrabLeftFlag())
        {
            animator.SetBool("IsLeftHandUp", true);

            isGrabbing = true;
        }
        else
        {
            animator.SetBool("IsLeftHandUp", false);

            // Reset object's mass...
            if (objectsRB != null)
            {
                objectsRB.mass = originalObjectMass;
                objectsRB = null;
            }

            // ...and eliminate fixed joint
            Destroy(GetComponent<FixedJoint>());

            isGrabbing = false;
        }
    }



    private void OnCollisionEnter(Collision col)
    {
        if (isGrabbing)
        {
            // Get rigidbody of object we want to grab
            objectsRB = col.transform.GetComponent<Rigidbody>();

            if (objectsRB != null)
            {

                // Create fixed joint on player
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;

                // Connect the object's rigidbody to player's new fixedjoint
                fj.connectedBody = objectsRB;

                // Store objects original mass and reduce current value (simulates strength) -> REVIEW THIS FOR OTHER UNPICKABLE OBJECTS
                originalObjectMass = objectsRB.mass;
                objectsRB.mass /= objectMassReduction;
            }
            else
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }

        }

    }

}
