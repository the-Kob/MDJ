using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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

    private void Update()
    {
        //if (!IsOwner) return;
        animator.SetBool("IsWalk", playerRag.isMoving); // Walking animation begin
        animator.SetBool("IsJumping", playerRag.desiresJump); // Jumping animation begin
    }
}