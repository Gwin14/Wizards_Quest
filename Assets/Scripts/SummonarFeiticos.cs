using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonarFeiticos : MonoBehaviour
{
    public GameObject bulletPrefab; // prefab da bala
    public GameObject bigBulletPrefab; // prefab da bala
    public float fireRate = 0.5f; // tempo entre os disparos
    public Transform firePoint; // ponto de origem da bala
    public int idBala = 0;
    public GameObject playerObject;
    private float nextFireTime; // tempo para o pr�ximo disparo



    private void Start()
    {
    }

    void Update()
    {

        FazerFeitico();
        // verificar se o bot�o esquerdo do mouse foi pressionado e se o tempo entre os disparos j� passou
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {

            switch (idBala)
            {
                case 0:
                    FeiticoBala();
                    break;

                case 1100:
                    FeiticoBolaGrande();
                    idBala = 0;
                    break;

                case 11100:
                    FeiticoVelocidade();
                    idBala = 0;
                    break;

                case 20000:
                    FeiticoDash();
                    idBala = 0;
                    break;

                default:
                    idBala = 0;
                    break;
            }


        }
    }

    void FeiticoBala()
    {
        // definir o tempo para o pr�ximo disparo com base na taxa de disparo
        nextFireTime = Time.time + fireRate;

        // obter a posi��o do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // criar a bala a partir do prefab e definir sua posi��o e rota��o
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // definir a velocidade da bala e sua dire��o em rela��o � posi��o do mouse
        Vector2 bulletDirection = (mousePosition - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 20;

        // destruir a bala depois de segundos
        Destroy(bullet, 2.0f);
    }

    void FeiticoBolaGrande()
    {
        // definir o tempo para o pr�ximo disparo com base na taxa de disparo
        nextFireTime = Time.time + fireRate;

        // obter a posi��o do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // criar a bala a partir do prefab e definir sua posi��o e rota��o
        GameObject bullet = Instantiate(bigBulletPrefab, firePoint.position, Quaternion.identity);

        // definir a velocidade da bala e sua dire��o em rela��o � posi��o do mouse
        Vector2 bulletDirection = (mousePosition - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 2;

        // destruir a bala depois de segundos
        Destroy(bullet, 100.0f);
    }

    void FeiticoVelocidade()
    {
        playerObject.GetComponent<PlayerController>().speed = 10.0f;
        Invoke("VoltarVelocidade", 5.0f);
    }

    //void VoltarVelocidade()
    //{
    //    playerObject.GetComponent<PlayerController>().speed = 5.0f;
    //}

    void FeiticoDash()
    {
        
    }

    private void FazerFeitico()
    {
        // verifica se a tecla "E" foi pressionada
        if (Input.GetKeyDown(KeyCode.E))
        {
            idBala += 100;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            idBala += 1000;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            idBala += 10000;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            idBala += 100000;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // verificar se a colis�o ocorreu com um objeto com a tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // reproduzir as part�culas de colis�o
            
        }
    }
}

