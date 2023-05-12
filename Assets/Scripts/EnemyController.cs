using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // velocidade de movimento do inimigo
    public float attackDistance = 5f; // distância mínima para atacar o jogador

    private Vector2 movementDirection; // direção do movimento do inimigo

    public float vida = 15;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float minTimeBetweenShots = 2f;
    public float maxTimeBetweenShots = 5f;

    private Transform playerTransform;
    private float timeUntilNextShot;

    // define a direção de movimento aleatória do inimigo
    private void SetRandomMovementDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        movementDirection = new Vector2(x, y).normalized;
    }

    // move o inimigo na direção definida
    private void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    // verifica a colisão com os obstáculos do mapa e define nova direção de movimento aleatória se houver colisão
    private void CheckCollision()
    {
        int layerMask = ~(1 << gameObject.layer); // Ignora a layer do próprio inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, 1f, layerMask);
        if (hit.collider != null)
        {
            SetRandomMovementDirection();
        }
    }

    // chama o método para definir nova direção de movimento aleatória periodicamente
    private IEnumerator RandomizeMovementDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f)); // define o tempo para mudar a direção
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

        // Verifica se o jogador está dentro da distância de ataque antes de atirar
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
        // Desenha uma esfera ao redor do inimigo para indicar a área de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}