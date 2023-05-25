using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CopyMotionNet : NetworkBehaviour
{
    public Transform targetLimb;
    public bool mirror;
    ConfigurableJoint cj;

    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        
    }

    void Update()
    {
        if (!IsOwner) return;

        if (!mirror)
            cj.targetRotation = targetLimb.rotation;
        else
            cj.targetRotation = Quaternion.Inverse(targetLimb.rotation);
    }
}
