using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // velocidade do jogador

    private Rigidbody2D rb; // referência ao componente Rigidbody2D do jogador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // obter referência ao componente Rigidbody2D do jogador
    }
    void FixedUpdate()
    {
        // obter a entrada de movimento do jogador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // definir a velocidade do jogador com base na entrada de movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
    }
}
