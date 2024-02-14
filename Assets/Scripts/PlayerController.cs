using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum EstadoPulo
{
    Pulando,
    Subindo,
    Caindo,
    Solo
}

public class PlayerMovement : MonoBehaviour
{

    //public float velocidadeQueda = 10f;
    //public float velocidadePulo = 10f;
    public float velocidade = 5f;
    public float tempoPulo = 1f;

    public float gravidade = 10f;


    // Controlador do personagem
    CharacterController cc;
    Vector3 movimentoJogador;
    EstadoPulo estadoPulo;

    private float raycastPes = 0.6f;
    private float raycastCabeca = 0.6f;

    // Inputs
    float horizontal;
    float vertical;
    bool teclaPulo;

    float tempoPercorrido = 0;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        estadoPulo = EstadoPulo.Solo;
    }

    void Update()
    {

        defineInputs();
        defineMovimentoJogador();
        checaEstadosDePulo();

        mostrarRaycasts();

    }

    // Define quais foram os inputs apertados
    void defineInputs()
    {
        // Verifica os inputs do usuário
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        teclaPulo = Input.GetKeyDown(KeyCode.Space) == true;
    }

    // Atualiza a nova posição do jogador e movimenta ele
    void defineMovimentoJogador()
    {
        // Define a velocidade e normaliza o tempo de atualização
        float direcao_x = horizontal * velocidade * Time.deltaTime;
        float direcao_z = vertical * velocidade * Time.deltaTime;
        float direcao_y = -gravidade; // Automaticamente puxa o jogador para baixo

        // Suaviza a velocidade de subida do pulo
        if (estadoPulo == EstadoPulo.Pulando)
            direcao_y = Mathf.SmoothStep(gravidade, gravidade * 0.30f, tempoPercorrido / tempoPulo);

        // Se estiver caindo, o cálculo da queda é mais suave
        if (estadoPulo == EstadoPulo.Caindo) 
            direcao_y = Mathf.SmoothStep(-gravidade * 0.20f, -gravidade, tempoPercorrido / tempoPulo);

        // normaliza o tempo de atualização do eixo Y
        direcao_y *= Time.deltaTime;

        // Atribui as direções necessárias para e movimenta o Character Controller
        movimentoJogador = new Vector3(direcao_x, direcao_y, direcao_z);
        cc.Move(movimentoJogador);

    }

    // Checa os estados do pulo e atribui ações a cada um deles
    void checaEstadosDePulo()
    {
        // Verifica se apertou espaço e se o jogador está no solo
        if (teclaPulo == true && estadoPulo == EstadoPulo.Solo)
            estadoPulo = EstadoPulo.Pulando; // Alterna o estado do pulo

        // Faz as verificações durante o pulo
        if (estadoPulo == EstadoPulo.Pulando)
        {
            // Atualização do tempo e altura do pulo
            if (tempoPercorrido < tempoPulo)
            {
                tempoPercorrido += gravidade * Time.deltaTime;
            }
            else
            { // Caso contrário, define que o jogador está caindo
                estadoPulo = EstadoPulo.Caindo;
                tempoPercorrido = 0;
            }
        }
        // Atualização do tempo e altura da queda
        if (estadoPulo == EstadoPulo.Caindo)
        {
            tempoPercorrido += gravidade * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // Redução do nome das variáveis
        Vector3 p = transform.position;
        Vector3 t = transform.localScale / 2;
        Quaternion r = transform.rotation;

        // Verifica o marcador de Raycast dos pés
        //if (Physics.Raycast(transform.position, Vector3.down, raycastPes))
        if (Physics.BoxCast(p, t, Vector3.down, r, raycastPes))
        {
            // Ao tocar no chão e identificar que está caindo, reseta o pulo
            if (estadoPulo == EstadoPulo.Caindo)
            {
                Debug.Log("Pulo resetado");
                estadoPulo = EstadoPulo.Solo;
                tempoPercorrido = 0;
            }
        }

        // Verifica o marcador de Raycast da cabeça
        Vector3 margemCabeca = new Vector3(0.5f, 0, 0.5f); // Deixa uma margem de erro pra cabeça
        if (Physics.BoxCast(p, t - margemCabeca, Vector3.up, r, raycastCabeca))
        {
            // Ao tocar no teto, define a animação de queda
            if (estadoPulo == EstadoPulo.Pulando)
            {
                Debug.Log("Bateu a cabeça em algo. Começou a cair...");
                estadoPulo = EstadoPulo.Caindo;
                tempoPercorrido = 0;
            }
        }
        
        
    }

    void mostrarRaycasts()
    {
        Debug.DrawRay(transform.position, Vector3.down * raycastPes, Color.red);
        Debug.DrawRay(transform.position, Vector3.up * raycastCabeca, Color.green);
    }


}
