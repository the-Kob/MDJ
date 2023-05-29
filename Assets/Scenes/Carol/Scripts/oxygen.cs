using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygen : MonoBehaviour
{
    private int oxygen_capacity;
    
    public int get_oxygen_capacity()
    {
        return oxygen_capacity;
    }

    public void decreaseOxygenCapacity()
    {
        if(oxygen_capacity > 0)
        {
            oxygen_capacity -= 25;
        }

        if(oxygen_capacity == 0)
        {
            //Debug.Log("oxygen capacity is 0");
        }

    }

    void Start()
    {
        oxygen_capacity = 100;
    }   

    void OnTriggerEnter(Collider other){
        if(gameObject.tag == "oxygen" && other.gameObject.tag == "Player"){
           // Debug.Log("oxygen collision");
            decreaseOxygenCapacity();
            
           // Debug.Log("oxygen capacity: " + get_oxygen_capacity());
        }
    }





}
