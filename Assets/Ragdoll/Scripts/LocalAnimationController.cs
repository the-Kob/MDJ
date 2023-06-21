using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LocalAnimationController : MonoBehaviour
{
    public Animator animator;
    public LocalPlayer playerRag;

    private void Update()
    {

        animator.SetBool("IsWalk", playerRag.Moving); // Walking animation begin
        animator.SetBool("IsJumping", playerRag.desiresJump); // Jumping animation begin
    }
}
