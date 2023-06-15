using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coll_green : MonoBehaviour
{
    void OnTriggerEnter( Collider other)
{
    if (other.gameObject.tag=="green" && gameObject.tag=="green" )
        
    {
        Debug.Log("green collision");
        destroyGameObject(other.gameObject);
        destroyGameObject(gameObject);
    }
    if(other.gameObject.tag=="purple" && gameObject.tag=="purple")
    {
        Debug.Log("purple collision");
       
        destroyGameObject(other.gameObject);
        destroyGameObject(gameObject);
    }
}

void OnTriggerExit( Collider other)
{
    if (other.gameObject.tag=="green" && gameObject.tag=="green")
    {
        Debug.Log("green collision");
    }
    if(other.gameObject.tag=="purple" && gameObject.tag=="purple")
    {
        Debug.Log("purple collision");
    }
}

void OnTriggerStay( Collider other)
{
    if (other.gameObject.tag=="green" && gameObject.tag=="green")
    {
        Debug.Log("green collision");
    }

    if(other.gameObject.tag=="purple" && gameObject.tag=="purple")
    {
        Debug.Log("purple collision");
    }
}

public void destroyGameObject( GameObject gameObject)
{
    Destroy(gameObject);
}
}
