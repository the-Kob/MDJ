using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_house : MonoBehaviour
{


    //get squares and if they both are null then open door (destroys self)
    [SerializeField]
    private GameObject green;
    
    [SerializeField]
    private GameObject purple;
    void Update()
    {
       if (green == null && purple == null)
        {
            Destroy(this.gameObject);
        }
    }
        
}

   

