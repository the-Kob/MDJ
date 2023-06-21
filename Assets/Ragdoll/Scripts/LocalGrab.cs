using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalGrab : MonoBehaviour
{
    [Header("Grab Settings")]
    public int objectMassReduction = 2;
    public bool isRightHand;
    public bool otherObjectResponsibleForMovement;

    [Header("References")]
    public GameObject playerMovementObject;
    public Animator animator;

    // Auxiliary variables
    private bool wantsToGrab;
    private float originalObjectMass;
    private Rigidbody objectsRB;
    private SpringJoint playersJoint;
    private bool grabbingOtherPlayer;
    
    [HideInInspector]
    public bool handOccupied;

    // INPUTS
    private bool grabLeftFlag;
    public void OnGrabLeft(InputAction.CallbackContext ctx) => grabLeftFlag = ctx.performed;

    private bool grabRightFlag;
    public void OnGrabRight(InputAction.CallbackContext ctx) => grabRightFlag = ctx.performed;


    void Update()
    {

        if (isRightHand)
        {
            if (grabRightFlag)
            {
                animator.SetBool("IsRightHandUp", true);
                wantsToGrab = true;
            }
            else
            {
                animator.SetBool("IsRightHandUp", false);

                // Make object responsible for movement also have a joint
                if (otherObjectResponsibleForMovement && (objectsRB == null || objectsRB.isKinematic || grabbingOtherPlayer))
                {
                    Destroy(playersJoint);
                    playersJoint = null;
                }

                // Reset object's mass...
                if (objectsRB != null)
                {
                    if (!grabbingOtherPlayer)
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
            if (grabLeftFlag)
            {
                animator.SetBool("IsLeftHandUp", true);
                wantsToGrab = true;
            }
            else
            {
                animator.SetBool("IsLeftHandUp", false);

                // Make object responsible for movement also have a joint
                if (otherObjectResponsibleForMovement && (objectsRB == null || objectsRB.isKinematic || grabbingOtherPlayer))
                {
                   Destroy(playersJoint);
                    playersJoint = null;
                }


                // Reset object's mass...
                if (objectsRB != null)
                {
                    if(!grabbingOtherPlayer)
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

            grabbingOtherPlayer = (col.gameObject.layer == LayerMask.NameToLayer("Player1") || col.gameObject.layer == LayerMask.NameToLayer("Player2"));

            if (objectsRB != null) // for objects
            {

                // Create fixed joint on player
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;

                // Connect the object's rigidbody to player's new fixedjoint
                fj.connectedBody = objectsRB;

                // Store objects original mass and reduce current value (simulates strength) -> REVIEW THIS FOR OTHER UNPICKABLE OBJECTS
                originalObjectMass = objectsRB.mass;

                if(!grabbingOtherPlayer)
                    objectsRB.mass /= objectMassReduction;
            }
            else // for walls and stuff
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                
            }

            if (otherObjectResponsibleForMovement && (objectsRB == null || objectsRB.isKinematic || grabbingOtherPlayer))
            {
                playersJoint = playerMovementObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
                playersJoint.spring = 20f;
            }

            handOccupied = true;

        }

    }

}
