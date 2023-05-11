using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // velocidade do jogador

    private Rigidbody2D rb; // referência ao componente Rigidbody2D do jogador

    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;

    public float vida = 100;

    private bool isDashing = false;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // obter referência ao componente Rigidbody2D do jogador

        vida = 100;
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

    public void Dano(float dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            print("gay");
            Destroy(gameObject);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashDistance / dashDuration * Time.fixedDeltaTime);
            dashTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

}
