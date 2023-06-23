using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeavyObject : MonoBehaviour
{
    private Rigidbody rb;

    private bool neilDetected;
    private bool umpaDetected;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        CheckForPlayers();
    }

    private void CheckForPlayers()
    {

        if (!(neilDetected && umpaDetected))
            rb.constraints = RigidbodyConstraints.FreezePosition;
        else
            rb.constraints = RigidbodyConstraints.None;

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Neil")
        {
            neilDetected = true;
        }
        if (other.tag == "Umpa")
        {
            umpaDetected = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Neil")
        {
            neilDetected = false;
            rb.freezeRotation = true;
        }
        if (other.tag == "Umpa")
        {
            umpaDetected = false;
            rb.freezeRotation = true;
        }
    }
}
