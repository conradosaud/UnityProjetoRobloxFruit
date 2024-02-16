using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    //public static bool estaLutando = false;
    bool podeAtacar = true;
    int golpe = 0;

    float tempoDecorrido = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if( InputController.inputAcaoPrincipal && podeAtacar == true )
        {
            proximoGolpe();
        }

    }

    void FixedUpdate()
    {
        if( podeAtacar == true )
            tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido > 1f)
        {
            golpe = 0;
            fimDoGolpe();
        }
    }

    void proximoGolpe()
    {
        golpe++;
        animator.SetInteger("estaLutandoPunhos", golpe);
        podeAtacar = false;

        if ( golpe >= 3)
        {
            golpe = 0;
            //estaLutando = false;
        }
        else
        {
            //estaLutando = true;
        }
        

    }

    void fimDoGolpe()
    {
        tempoDecorrido = 0;
        podeAtacar = true;
        animator.SetInteger("estaLutandoPunhos", golpe);
    }


}
