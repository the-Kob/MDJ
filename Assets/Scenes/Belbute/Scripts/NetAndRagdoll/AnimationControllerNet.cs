using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AnimationControllerNet : NetworkBehaviour
{
    public Animator animator;
    public Player playerRag;

    private void Update()
    {
        if (!IsOwner) return;

        animator.SetBool("IsWalk", playerRag.Moving); // Walking animation begin
        animator.SetBool("IsJumping", playerRag.desiredJump); // Jumping animation begin
    }
}
