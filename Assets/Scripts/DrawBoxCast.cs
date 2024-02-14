using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxCast : MonoBehaviour
{
    public static void Draw( Vector3 tamanhoDoObjeto, Vector3 posicao, Color color )
    {
        // Desenha as linhas representando o boxcast
        Vector3[] extremidades = new Vector3[8];

        // Calcula as oito extremidades do boxcast
        Vector3 halfExtents = tamanhoDoObjeto / 2f;
        extremidades[0] = posicao + new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        extremidades[1] = posicao + new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
        extremidades[2] = posicao + new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        extremidades[3] = posicao + new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);
        extremidades[4] = posicao + new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        extremidades[5] = posicao + new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
        extremidades[6] = posicao + new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        extremidades[7] = posicao + new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);

        // Desenha as linhas representando as arestas do boxcast
        Debug.DrawLine(extremidades[0], extremidades[1], color);
        Debug.DrawLine(extremidades[1], extremidades[3], color);
        Debug.DrawLine(extremidades[3], extremidades[2], color);
        Debug.DrawLine(extremidades[2], extremidades[0], color);
        Debug.DrawLine(extremidades[4], extremidades[5], color);
        Debug.DrawLine(extremidades[5], extremidades[7], color);
        Debug.DrawLine(extremidades[7], extremidades[6], color);
        Debug.DrawLine(extremidades[6], extremidades[4], color);
        Debug.DrawLine(extremidades[0], extremidades[4], color);
        Debug.DrawLine(extremidades[1], extremidades[5], color);
        Debug.DrawLine(extremidades[2], extremidades[6], color);
        Debug.DrawLine(extremidades[3], extremidades[7], color);
    }
}
