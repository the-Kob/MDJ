using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour
{

    void Start()
    {
        // Make cursor disappear and be locked in window
        Cursor.lockState = CursorLockMode.Locked;
    }

}
