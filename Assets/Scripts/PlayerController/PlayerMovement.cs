using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocidade = 5f;
    CharacterController cc; // Character Controller
    Vector3 movimentoJogador; // Nova posi��o do jogador calculada todo Update

    void Start()
    {
        // Inicializa��o das vari�veis
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        defineMovimentoJogador();
    }

    // Atualiza a nova posi��o do jogador e movimenta ele
    void defineMovimentoJogador()
    {
        // Define a velocidade e normaliza o tempo de atualiza��o
        float direcao_x = InputController.inputHorizontal * velocidade * Time.deltaTime;
        float direcao_z = InputController.inputVertical * velocidade * Time.deltaTime;
        float direcao_y = -PlayerManager.gravidade; // Automaticamente puxa o jogador para baixo

        // Suaviza a velocidade de subida do pulo
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
            direcao_y = 
                Mathf.SmoothStep(PlayerManager.gravidade, PlayerManager.gravidade * 0.30f, PlayerJump.suavidadePulo);

        // Se estiver caindo, o c�lculo da queda � mais suave
        if (PlayerJump.estadoPulo == EstadoPulo.Caindo)
            direcao_y = 
                Mathf.SmoothStep(-PlayerManager.gravidade * 0.20f, -PlayerManager.gravidade, PlayerJump.suavidadePulo);

        // Normaliza o tempo de atualiza��o do eixo Y
        direcao_y *= Time.deltaTime;

        // Movimenta o Character Controller
        movimentoJogador = new Vector3(direcao_x, direcao_y, direcao_z);
        cc.Move(movimentoJogador);

    }
}
