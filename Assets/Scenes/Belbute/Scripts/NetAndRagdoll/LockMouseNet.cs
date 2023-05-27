using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LockMouseNet : NetworkBehaviour
{

    void Start()
    {
        // Make cursor disappear and be locked in window
        Cursor.lockState = CursorLockMode.Locked;
    }

}
