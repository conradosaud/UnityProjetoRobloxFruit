using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Jumping,
    Falling
}

public class PlayerManager : MonoBehaviour
{

    // Altera a for�a que puxa o jogador ao ch�o
    // tamb�m � usado como for�a do pulo
    public static float gravidade = 10f;

    public static PlayerState playerState;

    void Start()
    {
        playerState = PlayerState.Idle;
    }

}
