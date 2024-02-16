using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    // Anima��es do jogador
    Animator animator; 

    // Vari�vel que pode ser usada por outras classes
    public static bool estaLutando = false;

    // Define se um novo ataque pode ser realizado
    bool podeAtacar = true;

    // Especifica��es do n�mero de golpes do combo
    int combo = 0;
    int comboMaximo = 3;

    // Especifica��es do tempo de ataque entre os golpes do combos
    float tempoMaximoEntreAtaques = 1f;
    float tempoDecorridoEntreAtaques = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Deteca se o jogar est� clicando
        if( InputController.inputAcaoPrincipal && podeAtacar == true )
            proximoGolpe();
    }

    void FixedUpdate()
    {
        // Aumenta o tempo decorrido entre os ataques, mas apenas se o jogador pode atacar
        if( podeAtacar == true )
            tempoDecorridoEntreAtaques += Time.deltaTime;
        // Se exceder o tempo, reseta e finaliza o combo
        if (tempoDecorridoEntreAtaques > tempoMaximoEntreAtaques)
        {
            combo = 0;
            fimDoGolpe();
        }
    }

    // Identifica qual � o pr�ximo golpe do combo
    void proximoGolpe()
    {
        // Se o combo exceder o limite, reseta ele. Se n�o, apenas +1 ao combo
        combo = combo >= comboMaximo ? 0 : combo + 1;

        // Configura as intera��es entre os golpes do combo
        animator.SetInteger("estaLutandoPunhos", combo);
        podeAtacar = false; 
        estaLutando = true;
    }

    // Essa fun��o � chamada pela pr�pria anima��o no final do frame de cada golpe
    void fimDoGolpe()
    {
        tempoDecorridoEntreAtaques = 0; // Zera o tempo de toler�ncia entre os golpes
        
        // Configura a anima��o
        podeAtacar = true;
        animator.SetInteger("estaLutandoPunhos", combo);

        // Verifica se o combo acabou/resetou para alterar o estado de luta do jogador
        if(combo == 0 )
            estaLutando = false;
    }


}
