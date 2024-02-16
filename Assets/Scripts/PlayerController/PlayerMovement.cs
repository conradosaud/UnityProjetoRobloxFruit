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

        // Calcula a direção vertical do movimento
        Vector3 direcao_vertical = Vector3.up * direcao_y;

        // https://www.youtube.com/watch?v=7kGCrq1cJew&ab_channel=iHeartGameDev

        // Obtém qual é a frente do personagem e qual é o lado direito dele
        Vector3 frente = Camera.main.transform.forward;
        Vector3 direita = Camera.main.transform.right;

        // Ignora o eixo vertical Y dos vetores, para o personagem não subir/voar
        frente.y = 0f; 
        direita.y = 0f;

        // Normaliza as direções para não correr mais rápido na diagonal
        frente.Normalize();
        direita.Normalize();

        // Aumenta os valores de acordo com as teclas pressionadas
        frente *= direcao_z;
        direita *= direcao_x;

        if (direcao_x != 0 || direcao_z != 0)
        {
            // Calcula a rotação com base na entrada do jogador

            float verticalAngle = Mathf.Atan2( frente.x + direita.x, frente.z + direita.z) * Mathf.Rad2Deg;
            //verticalAngle = Mathf.Atan2( -frente.x, -frente.z) * Mathf.Rad2Deg;
            Quaternion rotacaoDesejada = Quaternion.Euler(0, verticalAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, 0.15f);
        }

        // O resultado vai orientar o jogador na direção horizontal
        Vector3 movimento_horizontal = frente + direita;

        // Aplica o movimento horizontal e vertical ao Character Controller
        cc.Move(movimento_horizontal + direcao_vertical);

    }
}
