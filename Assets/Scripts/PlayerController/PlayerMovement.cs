using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocidade = 5f;
    CharacterController cc; // Character Controller
    Vector3 movimentoJogador; // Nova posição do jogador calculada todo Update

    void Start()
    {
        // Inicialização das variáveis
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        defineMovimentoJogador();
    }

    // Atualiza a nova posição do jogador e movimenta ele
    void defineMovimentoJogador()
    {
        // Define a velocidade e normaliza o tempo de atualização
        float direcao_x = InputController.inputHorizontal * velocidade * Time.deltaTime;
        float direcao_z = InputController.inputVertical * velocidade * Time.deltaTime;
        float direcao_y = -PlayerManager.gravidade; // Automaticamente puxa o jogador para baixo

        // Suaviza a velocidade de subida do pulo
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
            direcao_y = 
                Mathf.SmoothStep(PlayerManager.gravidade, PlayerManager.gravidade * 0.30f, PlayerJump.suavidadePulo);

        // Se estiver caindo, o cálculo da queda é mais suave
        if (PlayerJump.estadoPulo == EstadoPulo.Caindo)
            direcao_y = 
                Mathf.SmoothStep(-PlayerManager.gravidade * 0.20f, -PlayerManager.gravidade, PlayerJump.suavidadePulo);

        // Normaliza o tempo de atualização do eixo Y
        direcao_y *= Time.deltaTime;

        // Movimenta o Character Controller
        movimentoJogador = new Vector3(direcao_x, direcao_y, direcao_z);
        cc.Move(movimentoJogador);

    }
}
