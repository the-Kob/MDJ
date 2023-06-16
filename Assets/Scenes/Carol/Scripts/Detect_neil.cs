using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_neil : MonoBehaviour
{
    public static bool neil_detected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neil_detected = true;  
            Debug.Log("neil detected");
        }
        
    }

    private void Update()
    {
        if (detect_umpa.umpa_detected && neil_detected)
        {
            Destroy(this.gameObject);
        }
    }
}
