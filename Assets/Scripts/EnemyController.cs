using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // velocidade de movimento do inimigo

    private Vector2 movementDirection; // dire��o do movimento do inimigo

    public float vida = 10;

    public GameObject bulletPrefab; // Prefab da bala que o inimigo atira
    public float shootingInterval = 2f; // Intervalo entre os tiros
    public float bulletSpeed = 10f; // Velocidade da bala
    public Transform target; // Transform do jogador

    private float shootingTimer; // Timer para controlar o intervalo entre os tiros



    // define a dire��o de movimento aleat�ria do inimigo
    private void SetRandomMovementDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        movementDirection = new Vector2(x, y).normalized;
    }

    // move o inimigo na dire��o definida
    private void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    // verifica a colis�o com os obst�culos do mapa e define nova dire��o de movimento aleat�ria se houver colis�o
    private void CheckCollision()
    {
        int layerMask = ~(1 << gameObject.layer); // Ignora a layer do pr�prio inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, 1f, layerMask);
        if (hit.collider != null)
        {
            SetRandomMovementDirection();
        }
    }


    // chama o m�todo para definir nova dire��o de movimento aleat�ria periodicamente
    private IEnumerator RandomizeMovementDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f)); // define o tempo para mudar a dire��o
            SetRandomMovementDirection();
        }
    }

    void Start()
    {
        SetRandomMovementDirection();
        StartCoroutine(RandomizeMovementDirection());

        vida = 10;
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

    void Update()
    {
        Move();
        CheckCollision();

        if (target == null)
        {
            return;
        }

        shootingTimer -= Time.deltaTime;

        if (shootingTimer <= 0f)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }


    }


    void Shoot()
    {
        Vector2 shootingDirection = (target.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootingDirection * bulletSpeed;
    }

}
