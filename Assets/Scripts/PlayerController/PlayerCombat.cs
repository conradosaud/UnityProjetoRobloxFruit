using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    
    // Estado do combate para outros scripts identificarem
    public static bool estaLutando = false;

    // Configura��es de golpes do combo
    int combo = 0;
    int comboTotal = 3;
    bool podeAtacar = true; // Define se o pr�ximo golpe j� pode ser dado

    // Determina o tempo de dura��o entre cada golpe do combo
    float tempoMaximo = 1f;
    float tempoDecorrido = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if( InputController.inputAcaoPrincipal && podeAtacar == true )
            proximoGolpe();

    }

    void FixedUpdate()
    {
        // Contabiliza o tempo entre os golpes
        if( podeAtacar == true )
            tempoDecorrido += Time.deltaTime;
        // Verifica se o tempo entre os golpes expirou
        if (tempoDecorrido > tempoMaximo)
        {
            combo = 0; // Reseta a contagem do combo
            fimDoGolpe(); // Atualiza o Animator para que ele resete a anima��o
        }
    }

    // Inicia o pr�ximo golpe do combo
    void proximoGolpe()
    {
        // Passa para o pr�ximo golpe do combo
        combo++;

        // Atualiza a anima��o e os estados do combate
        animator.SetInteger("estaLutandoPunhos", combo);
        podeAtacar = false;
        estaLutando = true;

        // Reseta o combo se chegou no m�ximo
        if (combo >= comboTotal)
            combo = 0;

    }

    // Fun��o chamada no �ltimo frame de cada golpe do combo
    void fimDoGolpe()
    {
        tempoDecorrido = 0;
        podeAtacar = true;
        if (combo == 0)
        {
            animator.SetInteger("estaLutandoPunhos", combo);
            estaLutando = false;
        }
    }


}
