using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRed : MonoBehaviour
{
    [SerializeField]
    private GameObject redCube;
    
    public static bool redDetected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            redDetected = true;
            Debug.Log("Red detected");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
