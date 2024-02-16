using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if( PlayerManager.playerState == PlayerState.Idle)
        {
            Debug.Log("asdads");
            animator.SetBool("estaCorrendo", false);
        }
        if( PlayerManager.playerState == PlayerState.Moving)
        {
            animator.SetBool("estaCorrendo", true);
        }
    }
}
