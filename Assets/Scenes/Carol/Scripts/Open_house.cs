using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_house : MonoBehaviour
{


    //get childs and verifies if childs are missing or not, if yes then destroy the parent
    
    void Update()
    {
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
        if (allChildren.Length == 1)
        {
            Destroy(gameObject);
        }
        //else
        //{
            //Destroy(gameObject.transform.parent.gameObject);
        //}
    }
        
    }

   

