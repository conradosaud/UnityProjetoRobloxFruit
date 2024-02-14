using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum EstadoPulo_Backup
{
    Pulando,
    Subindo,
    Caindo,
    Solo
}

public class PlayerController_Backup : MonoBehaviour
{

    //public float velocidadeQueda = 10f;
    //public float velocidadePulo = 10f;
    public float velocidade = 5f;
    public float tempoPulo = 1f;

    public float gravidade = 10f;


    // Controlador do personagem
    CharacterController cc;
    Vector3 movimentoJogador;
    EstadoPulo_Backup estadoPulo_Backup;

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
        estadoPulo_Backup = EstadoPulo_Backup.Solo;
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
        // Verifica os inputs do usu�rio
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        teclaPulo = Input.GetKeyDown(KeyCode.Space) == true;
    }

    // Atualiza a nova posi��o do jogador e movimenta ele
    void defineMovimentoJogador()
    {
        // Define a velocidade e normaliza o tempo de atualiza��o
        float direcao_x = horizontal * velocidade * Time.deltaTime;
        float direcao_z = vertical * velocidade * Time.deltaTime;
        float direcao_y = -gravidade; // Automaticamente puxa o jogador para baixo

        // Suaviza a velocidade de subida do pulo
        if (estadoPulo_Backup == EstadoPulo_Backup.Pulando)
            direcao_y = Mathf.SmoothStep(gravidade, gravidade * 0.30f, tempoPercorrido / tempoPulo);

        // Se estiver caindo, o c�lculo da queda � mais suave
        if (estadoPulo_Backup == EstadoPulo_Backup.Caindo)
            direcao_y = Mathf.SmoothStep(-gravidade * 0.20f, -gravidade, tempoPercorrido / tempoPulo);

        // normaliza o tempo de atualiza��o do eixo Y
        direcao_y *= Time.deltaTime;

        // Atribui as dire��es necess�rias para e movimenta o Character Controller
        movimentoJogador = new Vector3(direcao_x, direcao_y, direcao_z);
        cc.Move(movimentoJogador);

    }

    // Checa os estados do pulo e atribui a��es a cada um deles
    void checaEstadosDePulo()
    {
        // Verifica se apertou espa�o e se o jogador est� no solo
        if (teclaPulo == true && estadoPulo_Backup == EstadoPulo_Backup.Solo)
            estadoPulo_Backup = EstadoPulo_Backup.Pulando; // Alterna o estado do pulo

        // Faz as verifica��es durante o pulo
        if (estadoPulo_Backup == EstadoPulo_Backup.Pulando)
        {
            // Atualiza��o do tempo e altura do pulo
            if (tempoPercorrido < tempoPulo)
            {
                tempoPercorrido += gravidade * Time.deltaTime;
            }
            else
            { // Caso contr�rio, define que o jogador est� caindo
                estadoPulo_Backup = EstadoPulo_Backup.Caindo;
                tempoPercorrido = 0;
            }
        }
        // Atualiza��o do tempo e altura da queda
        if (estadoPulo_Backup == EstadoPulo_Backup.Caindo)
        {
            tempoPercorrido += gravidade * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // Redu��o do nome das vari�veis
        Vector3 p = transform.position;
        Vector3 t = transform.localScale / 2;
        Quaternion r = transform.rotation;

        // Verifica o marcador de Raycast dos p�s
        //if (Physics.Raycast(transform.position, Vector3.down, raycastPes))
        if (Physics.BoxCast(p, t, Vector3.down, r, raycastPes))
        {
            // Ao tocar no ch�o e identificar que est� caindo, reseta o pulo
            if (estadoPulo_Backup == EstadoPulo_Backup.Caindo)
            {
                Debug.Log("Pulo resetado");
                estadoPulo_Backup = EstadoPulo_Backup.Solo;
                tempoPercorrido = 0;
            }
        }

        // Verifica o marcador de Raycast da cabe�a
        Vector3 margemCabeca = new Vector3(0.5f, 0, 0.5f); // Deixa uma margem de erro pra cabe�a
        if (Physics.BoxCast(p, t - margemCabeca, Vector3.up, r, raycastCabeca))
        {
            // Ao tocar no teto, define a anima��o de queda
            if (estadoPulo_Backup == EstadoPulo_Backup.Pulando)
            {
                Debug.Log("Bateu a cabe�a em algo. Come�ou a cair...");
                estadoPulo_Backup = EstadoPulo_Backup.Caindo;
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