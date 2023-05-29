using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AnimationControllerNet : NetworkBehaviour
{
    public float sensitivityToInput = 0.1f;
    public Animator animator;
    public Player playerRag;

    private void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        //playerRag = GetComponentInChildren<Player>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        animator.SetBool("IsWalk", playerRag.Moving); // Walking animation begin
       // animator.SetBool("IsJumping", playerRag.desiresJump); // Jumping animation begin
    }
}
