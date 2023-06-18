using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlue : MonoBehaviour
{
    [SerializeField]
    private GameObject blueCube;
    
    public static bool blueDetected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blue")
        {
            blueDetected = true;
            Debug.Log("Blue detected");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
