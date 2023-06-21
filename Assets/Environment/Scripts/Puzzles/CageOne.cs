using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class CageOne : MonoBehaviour
{
    [SerializeField] private GameObject neilGround;
    [SerializeField] private GameObject umpaGround;

    private void Update()
    {
        if(neilGround == null && umpaGround == null)
            Destroy(this.gameObject);
    }
}
