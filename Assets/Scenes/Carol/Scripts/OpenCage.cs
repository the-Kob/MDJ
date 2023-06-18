
using System;
using UnityEngine;

public class OpenCage : MonoBehaviour
{
    [SerializeField] private GameObject redGround;
    [SerializeField] private GameObject blueGround;
    [SerializeField] private GameObject orangeGround;

    private void Update()
    {
        if(blueGround == null && orangeGround == null && redGround == null)
            Destroy(this.gameObject);
    }
}
