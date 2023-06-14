using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Grab : MonoBehaviour
{
    [Header("Grab Settings")]
    public int objectMassReduction = 2;
    public bool isRightHand;

    [Header("References")]
    public Animator animator;

    // Auxiliary variables
    private bool wantsToGrab;
    private float originalObjectMass;
    private Rigidbody objectsRB;
    private bool handOccupied;



    void Update()
    {

        if (isRightHand)
        {
            if (InputManager.Instance.GetGrabRightFlag())
            {
                animator.SetBool("IsRightHandUp", true);
                wantsToGrab = true;
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

                wantsToGrab = false;
                handOccupied = false;
            }

        }
        else
        {
            if (InputManager.Instance.GetGrabLeftFlag())
            {
                animator.SetBool("IsLeftHandUp", true);
                wantsToGrab = true;
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

                wantsToGrab = false;
                handOccupied = false;
            }

        }
    }



    private void OnCollisionEnter(Collision col)
    {
        if (wantsToGrab && !handOccupied)
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

            handOccupied = true;

        }

    }

}
