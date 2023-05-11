using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DestructableObjects : MonoBehaviour
{

    public float vida = 5.0f;

    public void Dano(float dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            print("gay");
            Destroy(gameObject);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
