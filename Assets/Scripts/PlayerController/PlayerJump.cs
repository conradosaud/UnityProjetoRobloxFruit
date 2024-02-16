using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum para classificar os estados de pulo do jogador
public enum EstadoPulo
{
    Pulando,
    Subindo,
    Caindo,
    Solo
}

public class PlayerJump : MonoBehaviour
{

    // Controladores do personagem
    CharacterController cc; // Character Controller
    public static EstadoPulo estadoPulo; // Define o estado atual do pulo do jogador

    // C�lculo do raycast emitido pelo jogador
    private float raycastPes = 0.6f;
    private float raycastCabeca = 0.6f;

    // Vari�vel auxiliar para calcular e resetar o tempo do pulo
    public float tempoPulo = 3f;
    float tempoPercorrido = 0;
    public static float suavidadePulo = 0;

    void Start()
    {
        // Inicializa��o das vari�veis
        cc = GetComponent<CharacterController>();
        estadoPulo = EstadoPulo.Solo;
    }

    // Update is called once per frame
    void Update()
    {
        checaEstadosDePulo();
    }

    // Checa os estados do pulo e atribui a��es a cada um deles
    void checaEstadosDePulo()
    {
        // Verifica se apertou espa�o e se o jogador est� no solo
        if (InputController.inputPulo == true && estadoPulo == EstadoPulo.Solo)
        {
            Debug.Log("salve");
            estadoPulo = EstadoPulo.Pulando; // Alterna o estado do pulo
        }

        // Faz as verifica��es durante o pulo
        if (estadoPulo == EstadoPulo.Pulando)
        {
            // Atualiza��o do tempo e altura do pulo
            if (tempoPercorrido < tempoPulo)
            {
                tempoPercorrido += PlayerManager.gravidade * Time.deltaTime;
            }
            else
            { // Caso contr�rio, define que o jogador est� caindo
                estadoPulo = EstadoPulo.Caindo;
                tempoPercorrido = 0;
            }
        }
        // Atualiza��o do tempo e altura da queda
        if (estadoPulo == EstadoPulo.Caindo)
        {
            tempoPercorrido += PlayerManager.gravidade * Time.deltaTime;
        }

        // Atribui a suavidade do intervalo entre pulos para o PlayerMovement
        suavidadePulo = tempoPercorrido / tempoPulo;

    }

    // Mostra os pontos de raycast do jogador. �til para visualiza��o de testes
    void mostrarRaycasts()
    {
        Debug.DrawRay(transform.position, Vector3.down * raycastPes, Color.red);
        Debug.DrawRay(transform.position, Vector3.up * raycastCabeca, Color.green);
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
            if (estadoPulo == EstadoPulo.Caindo)
            {
                Debug.Log("Pulo resetado");
                estadoPulo = EstadoPulo.Solo;
                tempoPercorrido = 0;
            }
        }

        // Verifica o marcador de Raycast da cabe�a
        Vector3 margemCabeca = new Vector3(0.5f, 0, 0.5f); // Deixa uma margem de erro pra cabe�a
        if (Physics.BoxCast(p, t - margemCabeca, Vector3.up, r, raycastCabeca))
        {
            // Ao tocar no teto, define a anima��o de queda
            if (estadoPulo == EstadoPulo.Pulando)
            {
                Debug.Log("Bateu a cabe�a em algo. Come�ou a cair...");
                estadoPulo = EstadoPulo.Caindo;
                tempoPercorrido = 0;
            }
        }


    }

}
