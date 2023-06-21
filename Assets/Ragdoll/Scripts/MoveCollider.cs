using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{
    [SerializeField]
    SphereCollider sc;

    [SerializeField]
    Transform transformToFollow;

    void Update()
    {
        sc.center = transform.InverseTransformPoint(transformToFollow.position);
        
    }
}
