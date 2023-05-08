using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public float lifetime = 2.0f; // tempo de vida da bala em segundos

    void Start()
    {
        // destruir a bala após o tempo de vida especificado
        Destroy(gameObject, lifetime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
