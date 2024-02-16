using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    // Animações do jogador
    Animator animator; 

    // Variável que pode ser usada por outras classes
    public static bool estaLutando = false;

    // Define se um novo ataque pode ser realizado
    bool podeAtacar = true;

    // Especificações do número de golpes do combo
    int combo = 0;
    int comboMaximo = 3;

    // Especificações do tempo de ataque entre os golpes do combos
    float tempoMaximoEntreAtaques = 1f;
    float tempoDecorridoEntreAtaques = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Deteca se o jogar está clicando
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

    // Identifica qual é o próximo golpe do combo
    void proximoGolpe()
    {
        // Se o combo exceder o limite, reseta ele. Se não, apenas +1 ao combo
        combo = combo >= comboMaximo ? 0 : combo + 1;

        // Configura as interações entre os golpes do combo
        animator.SetInteger("estaLutandoPunhos", combo);
        podeAtacar = false; 
        estaLutando = true;
    }

    // Essa função é chamada pela própria animação no final do frame de cada golpe
    void fimDoGolpe()
    {
        tempoDecorridoEntreAtaques = 0; // Zera o tempo de tolerância entre os golpes
        
        // Configura a animação
        podeAtacar = true;
        animator.SetInteger("estaLutandoPunhos", combo);

        // Verifica se o combo acabou/resetou para alterar o estado de luta do jogador
        if(combo == 0 )
            estaLutando = false;
    }


}
