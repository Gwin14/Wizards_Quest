using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // velocidade de movimento do inimigo
    public float attackDistance = 5f; // dist�ncia m�nima para atacar o jogador

    private Vector2 movementDirection; // dire��o do movimento do inimigo

    public float vida = 15;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float minTimeBetweenShots = 2f;
    public float maxTimeBetweenShots = 5f;

    private Transform playerTransform;
    private float timeUntilNextShot;

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

        vida = 15;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timeUntilNextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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

        timeUntilNextShot -= Time.deltaTime;

        // Verifica se o jogador est� dentro da dist�ncia de ataque antes de atirar
        if (timeUntilNextShot <= 0f && Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            Shoot();
            timeUntilNextShot = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void Shoot()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }


    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera ao redor do inimigo para indicar a �rea de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}