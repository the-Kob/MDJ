using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect_umpa : MonoBehaviour
{
    public static bool umpa_detected = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Umpa")
        {
          umpa_detected = true;  
          Debug.Log("umpa detected");
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Umpa")
        {
            umpa_detected = false;  
            Debug.Log("umpa false");
            
        }
    }
    
    private void Update()
    {
        if (Detect_neil.neil_detected && umpa_detected)
        {
            Destroy(this.gameObject);
        }
    }
}
