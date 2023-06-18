using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOrange : MonoBehaviour
{
    [SerializeField]
    private GameObject orangeCube;
    
    public static bool orangeDetected = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Orange")
        {
            orangeDetected = true;
            Debug.Log("Orange detected");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
