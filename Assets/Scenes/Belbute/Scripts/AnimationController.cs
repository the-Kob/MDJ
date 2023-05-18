using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float sensitivityToInput = 0.1f;
    Animator animator;
    PlayerRag playerRag;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerRag = GetComponentInChildren<PlayerRag>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsWalk", playerRag.isMoving); // Animation begin
        animator.SetBool("IsJumping", playerRag.desiredJump); // Animation begin
    }
}
