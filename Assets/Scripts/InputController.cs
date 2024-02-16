using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public static float inputHorizontal = 0;
    public static float inputVertical = 0;
    public static bool inputPulo = false;
    public static bool inputAcaoPrincipal = false;
    public static bool inputAcaoSecundaria = false;

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputPulo = Input.GetKeyDown(KeyCode.Space) == true; // Definir controle para Joystick
        inputAcaoPrincipal = Input.GetAxis("Fire1") != 0;
        inputAcaoSecundaria = Input.GetAxis("Fire2") != 0;
    }
}
