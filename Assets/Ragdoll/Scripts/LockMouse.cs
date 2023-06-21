using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

}
