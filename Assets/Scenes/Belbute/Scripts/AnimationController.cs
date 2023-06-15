using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public SinglePlayer playerRag;

    private void Update()
    {

        animator.SetBool("IsWalk", playerRag.Moving); // Walking animation begin
        animator.SetBool("IsJumping", playerRag.desiredJump); // Jumping animation begin
    }
}
