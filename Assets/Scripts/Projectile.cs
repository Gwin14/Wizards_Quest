using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    public GameObject bulletexplosion;
    public Transform bulletExplosionSpawnPoint;


    void Start()
    {

    }
    void Update()
    {
    }


    //void OnBecameInvisible()
    //{
    //    // destrói o objeto quando ele sai da tela
    //    Destroy(gameObject);
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // verifica se o projétil colidiu com um inimigo
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject bullet = Instantiate(bulletexplosion, bulletExplosionSpawnPoint.position, Quaternion.identity);

            // adiciona código para aplicar dano ao inimigo
            Destroy(gameObject);
        }
    }
}
