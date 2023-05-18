using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    private PlayerRag pr;
   
    void Start()
    {
        // FindObjectOfType finds object in hierarchy with PlayerController
        pr = GameObject.FindObjectOfType<PlayerRag>().GetComponent<PlayerRag>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        pr.isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        pr.isGrounded = false;
    }

}
