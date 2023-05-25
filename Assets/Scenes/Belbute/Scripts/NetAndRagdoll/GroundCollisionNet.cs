using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GroundCollisionNet : NetworkBehaviour
{
    private TestNet pr;
   
    void Start()
    {
        // FindObjectOfType finds object in hierarchy with PlayerController
        pr = GameObject.FindObjectOfType<TestNet>().GetComponent<TestNet>();
    }


    private void OnTriggerEnter(Collider other)
    {
        pr.isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        pr.isGrounded = false;
    }



}
