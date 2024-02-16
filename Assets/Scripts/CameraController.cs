using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform jogador;

    float mouseX;
    float mouseY;

    float sensibilidadeMouse = 5f;

    void Start()
    {

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
        // Mant�m o mouse preso dentro da janela do jogo
        //Cursor.lockState = CursorLockMode.Confined;

        jogador = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        transform.position = jogador.position - new Vector3(0, -0.58f, 0);

        if (!InputController.inputAcaoSecundaria)
            return;

        // Obt�m a posi��o do mouse no tela
        mouseX += Input.GetAxis("Mouse X") * sensibilidadeMouse;
        mouseY -= Input.GetAxis("Mouse Y") * sensibilidadeMouse;

        // Limita a rota��o at� um �ngulo espec�fico
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Roda a c�mera e o jogador na dire��o do mouse
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //jogador.rotation = Quaternion.Euler(0, mouseX, 0);

    }

}
