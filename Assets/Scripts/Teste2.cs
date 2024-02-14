using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            transform.GetComponent<Rigidbody>().AddForce ( Vector3.up * 10f, ForceMode.Impulse);
        }
    }
}
