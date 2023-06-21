using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_neil : MonoBehaviour
{
    public static bool neil_detected;

    public void Start()
    {
        neil_detected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            neil_detected = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            neil_detected = false;              
        }
    }


    private void FixedUpdate()
    {
       
        if (detect_umpa.umpa_detected && neil_detected)
        {
            Destroy(this.gameObject);
        }
    }
}
