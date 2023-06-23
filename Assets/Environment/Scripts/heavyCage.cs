using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyCage : MonoBehaviour
{
    
        [SerializeField] private GameObject heavyGround;

        private void Update()
        {
            if(heavyGround == null)
                Destroy(this.gameObject);
        }
    }

