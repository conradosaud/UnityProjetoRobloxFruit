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

    // Altera a força que puxa o jogador ao chão
    // também é usado como força do pulo
    public static float gravidade = 10f;

    public static PlayerState playerState;

    void Start()
    {
        playerState = PlayerState.Idle;
    }

}
