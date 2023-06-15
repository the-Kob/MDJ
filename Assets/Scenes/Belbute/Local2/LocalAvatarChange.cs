using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalAvatarChange : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;
    public GameObject playerPrefabB;

    private PlayerInputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();

    }


    void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Player joined");

        if (playerA == null)
        {
            playerA = input.gameObject;
            inputManager.playerPrefab = playerPrefabB;
        }
        else
        {
            playerB = input.gameObject;
        }
    }
}
