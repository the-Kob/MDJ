using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    private PlayerController playerController;
   
    void Start()
    {
        // FindObjectOfType finds object in hierarchy with PlayerController
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.isGrounded = true;
    }

}
