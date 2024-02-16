using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    CharacterController cc;
    Animator animator;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if( PlayerJump.estadoPulo == EstadoPulo.Pulando || PlayerJump.estadoPulo == EstadoPulo.Caindo)
        {
            animator.SetBool("estaPulando", true);
        }
        else
        {
            animator.SetBool("estaPulando", false);

            if( cc.velocity.magnitude > 1)
            {
                animator.SetBool("estaCorrendo", true);
            }
            else
            {
                animator.SetBool("estaCorrendo", false);
            }
        }


    }
}
