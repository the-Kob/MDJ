using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        rb.isKinematic = !(neilDetected && umpaDetected);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = true;
            Debug.Log("neil detected");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = true;
            Debug.Log("umpa detected");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = false;
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = false;
        }
    }
}
