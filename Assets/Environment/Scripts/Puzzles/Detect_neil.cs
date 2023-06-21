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
        Debug.Log(neil_detected);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neil_detected = true;  
            Debug.Log("neil detected");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neil_detected = false;  
            Debug.Log("neil false");
            
        }
    }


    private void FixedUpdate()
    {
       
        //Debug.Log(neil_detected);
        if (detect_umpa.umpa_detected && neil_detected)
        {
            Destroy(this.gameObject);
        }
    }
}
