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

    // Cálculo do raycast emitido pelo jogador
    private float raycastPes = 0.6f;
    private float raycastCabeca = 0.6f;

    // Variável auxiliar para calcular e resetar o tempo do pulo
    public float tempoPulo = 3f;
    float tempoPercorrido = 0;
    public static float suavidadePulo = 0;

    void Start()
    {
        // Inicialização das variáveis
        cc = GetComponent<CharacterController>();
        estadoPulo = EstadoPulo.Solo;
    }

    // Update is called once per frame
    void Update()
    {
        checaEstadosDePulo();
    }

    // Checa os estados do pulo e atribui ações a cada um deles
    void checaEstadosDePulo()
    {
        // Verifica se apertou espaço e se o jogador está no solo
        if (InputController.inputPulo == true && estadoPulo == EstadoPulo.Solo)
        {
            Debug.Log("salve");
            estadoPulo = EstadoPulo.Pulando; // Alterna o estado do pulo
        }

        // Faz as verificações durante o pulo
        if (estadoPulo == EstadoPulo.Pulando)
        {
            // Atualização do tempo e altura do pulo
            if (tempoPercorrido < tempoPulo)
            {
                tempoPercorrido += PlayerManager.gravidade * Time.deltaTime;
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
            tempoPercorrido += PlayerManager.gravidade * Time.deltaTime;
        }

        // Atribui a suavidade do intervalo entre pulos para o PlayerMovement
        suavidadePulo = tempoPercorrido / tempoPulo;

    }

    // Mostra os pontos de raycast do jogador. Útil para visualização de testes
    void mostrarRaycasts()
    {
        Debug.DrawRay(transform.position, Vector3.down * raycastPes, Color.red);
        Debug.DrawRay(transform.position, Vector3.up * raycastCabeca, Color.green);
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

}
