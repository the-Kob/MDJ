using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Orbit : MonoBehaviour
{
    public GameObject planet;
    public float speed;

    private void Start()
    {
        //planet = GameObject.Find("Planet");
    }

    void Update()
    {
        OrbitAround();
        
    }
    
    public void OrbitAround()
    {
        transform.RotateAround(planet.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
