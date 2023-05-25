using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    private PlayerRag pr;
   
    void Start()
    {
        // FindObjectOfType finds object in hierarchy with PlayerController
        pr = GameObject.FindObjectOfType<PlayerRag>().GetComponent<PlayerRag>();
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
