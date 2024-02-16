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

        // Calcula a dire��o vertical do movimento
        Vector3 direcao_vertical = Vector3.up * direcao_y;

        // https://www.youtube.com/watch?v=7kGCrq1cJew&ab_channel=iHeartGameDev

        // Obt�m qual � a frente do personagem e qual � o lado direito dele
        Vector3 frente = Camera.main.transform.forward;
        Vector3 direita = Camera.main.transform.right;

        // Ignora o eixo vertical Y dos vetores, para o personagem n�o subir/voar
        frente.y = 0f; 
        direita.y = 0f;

        // Normaliza as dire��es para n�o correr mais r�pido na diagonal
        frente.Normalize();
        direita.Normalize();

        // Aumenta os valores de acordo com as teclas pressionadas
        frente *= direcao_z;
        direita *= direcao_x;

        if (direcao_x != 0 || direcao_z != 0)
        {
            // Calcula a rota��o com base na entrada do jogador

            float verticalAngle = Mathf.Atan2( frente.x + direita.x, frente.z + direita.z) * Mathf.Rad2Deg;
            //verticalAngle = Mathf.Atan2( -frente.x, -frente.z) * Mathf.Rad2Deg;
            Quaternion rotacaoDesejada = Quaternion.Euler(0, verticalAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, 0.15f);
        }

        // O resultado vai orientar o jogador na dire��o horizontal
        Vector3 movimento_horizontal = frente + direita;

        // Aplica o movimento horizontal e vertical ao Character Controller
        cc.Move(movimento_horizontal + direcao_vertical);

    }
}
