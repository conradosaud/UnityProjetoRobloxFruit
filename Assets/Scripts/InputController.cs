using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public static float inputHorizontal = 0;
    public static float inputVertical = 0;
    public static bool inputPulo = false;
    public static bool inputAcao = false;

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputPulo = Input.GetKeyDown(KeyCode.Space) == true; // Definir controle para Joystick
        inputAcao = Input.GetAxis("Fire1") != 0;
    }
}
