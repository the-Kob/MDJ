using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnlockTrampoline : MonoBehaviour
{
    [SerializeField] private GameObject component1;
    [SerializeField] private GameObject component2;
    [SerializeField] private GameObject component3;
    [SerializeField] private GameObject component4;
    [SerializeField] private GameObject component5;
    
    void Update()
    {
        if (component1 == null && component2 == null && component3 == null && component4 == null && component5 == null)
        {
            Destroy(this.gameObject);
        }
    }
}
